namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Parser;
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Groups the style rules of a collection by the id, class, tag name, or
    /// attribute that the subject (i.e., the rightmost compound) of their selector
    /// requires. An element can only be matched by the rules in the groups of its
    /// own id, classes, tag name, and attributes, plus the rules that could not be
    /// grouped. Without the index every rule would be matched against every
    /// element. Pseudo-elements only need the rules targeting pseudo-elements.
    /// </summary>
    sealed class StyleRuleIndex
    {
        #region Fields

        private readonly KeyedRules _keys = new();
        // the rules targeting pseudo-elements, again grouped by the element they belong to
        private readonly KeyedRules _pseudoKeys = new();
        private readonly List<Int32> _pseudo = new();
        // the rules targeting pseudo-elements that may declare their content
        private readonly KeyedRules _contentKeys = new();
        private Int32 _contentCount;

        #endregion

        #region ctor

        public StyleRuleIndex(IReadOnlyList<ICssStyleRule> rules)
        {
            for (var i = 0; i < rules.Count; i++)
            {
                Add(rules[i], i);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Collects the (ascending) indices of the rules that may match the
        /// given element.
        /// </summary>
        public Candidates GetCandidates(IElement element)
        {
            if (element is IPseudoElement)
            {
                // a pseudo-element is matched by the rules for the element it belongs to
                return element is PseudoElement { Host: { } host }
                    ? Collect(host, _pseudoKeys)
                    : new Candidates(_pseudo, null);
            }

            return Collect(element, _keys);
        }

        /// <summary>
        /// Checks if a rule that may match the given pseudo-element declares its
        /// content. Without content a ::before or ::after has no box, and rules
        /// such as *::before only setting other properties do not change that.
        /// </summary>
        public Boolean MayDeclareContent(IElement pseudoElement)
        {
            if (_contentCount == 0)
            {
                return false;
            }

            return pseudoElement is not PseudoElement { Host: { } host } || Collect(host, _contentKeys).Count > 0;
        }

        #endregion

        #region Helpers

        private void Add(ICssStyleRule rule, Int32 index)
        {
            // nested rules are matched independently of their parent's selector
            var hasNested = rule.Rules.Length > 0;

            // a rule without a (valid) selector matches nothing, but its nested rules may
            if (!hasNested && rule.Selector is null && rule is CssStyleRule)
            {
                return;
            }

            var info = SelectorInfo.Get(rule);
            var keys = hasNested ? null : info.Keys;

            if (hasNested || info.TargetsPseudo)
            {
                _pseudo.Add(index);
                _pseudoKeys.Add(keys, index);

                // a nested rule is matched on its own, so it may declare the content
                if (hasNested || DeclaresContent(rule.Style))
                {
                    _contentKeys.Add(keys, index);
                    _contentCount++;
                }
            }

            _keys.Add(keys, index);
        }

        private static Boolean DeclaresContent(ICssStyleDeclaration style) => style is CssStyleDeclaration declaration ?
            declaration.GetDeclaration(PropertyNames.Content) is not null :
            style.Any(m => PropertyNames.Content.Equals(m.Name, StringComparison.OrdinalIgnoreCase));

        private static Candidates Collect(IElement element, KeyedRules keyed)
        {
            // The ungrouped rules (e.g., [data-theme] or :root) are candidates for every
            // element, hence they are not copied, but merged with the grouped ones while
            // iterating. Only the (few) groups of the element are combined, if needed.
            List<Int32>? first = null;
            List<Int32>? combined = null;
            AddGroups(element, keyed, ref first, ref combined);

            // the rules for children of an element with a key, e.g., .list > *
            if (keyed.ParentRules is { } parentRules && element.ParentElement is { } parent)
            {
                AddGroups(parent, parentRules, ref first, ref combined);
            }

            // the rules for descendants of an element with a key, e.g., .prose *
            if (keyed.AncestorRules is { } ancestorRules)
            {
                for (var ancestor = element.ParentElement; ancestor is not null; ancestor = ancestor.ParentElement)
                {
                    AddGroups(ancestor, ancestorRules, ref first, ref combined);
                }
            }

            if (combined is not null)
            {
                // a rule may be found via several keys, e.g., ".a, .b" for class="a b"
                combined.Sort();
                var count = 0;

                for (var i = 0; i < combined.Count; i++)
                {
                    if (count == 0 || combined[count - 1] != combined[i])
                    {
                        combined[count++] = combined[i];
                    }
                }

                combined.RemoveRange(count, combined.Count - count);
                first = combined;
            }

            return new Candidates(keyed.Ungrouped, first);
        }

        /// <summary>
        /// Adds the groups of the keys the element has.
        /// </summary>
        private static void AddGroups(IElement element, KeyedRules keyed, ref List<Int32>? first, ref List<Int32>? combined)
        {
            var id = element.Id;

            // e.g., the hundreds of rules declaring the custom properties of a design system
            if (keyed.Root.Count > 0 && ((element.ParentElement is null && element.Owner?.DocumentElement == element) ||
                PseudoClassStateStore.TryGet(element, PseudoClassNames.Root, out _)))
            {
                AddGroup(ref first, ref combined, keyed.Root);
            }

            if (!String.IsNullOrEmpty(id) && keyed.Ids.TryGetValue(id!, out var rules))
            {
                AddGroup(ref first, ref combined, rules);
            }

            if (keyed.Classes.Count > 0 && element.HasAttribute(AttributeNames.Class))
            {
                // by index, as enumerating the list allocates (for every element and its ancestors)
                var classes = element.ClassList;

                for (var i = 0; i < classes.Length; i++)
                {
                    if (classes[i] is { } name && keyed.Classes.TryGetValue(name, out rules))
                    {
                        AddGroup(ref first, ref combined, rules);
                    }
                }
            }

            if (keyed.Tags.TryGetValue(element.LocalName, out rules))
            {
                AddGroup(ref first, ref combined, rules);
            }

            if (keyed.Attributes.Count > 0)
            {
                foreach (var attribute in element.Attributes)
                {
                    if (keyed.Attributes.TryGetValue(attribute.Name, out rules))
                    {
                        AddGroup(ref first, ref combined, rules);
                    }
                }
            }
        }

        private static void AddGroup(ref List<Int32>? first, ref List<Int32>? combined, List<Int32> rules)
        {
            if (first is null)
            {
                // a single group is already sorted and free of duplicates
                first = rules;
            }
            else
            {
                if (combined is null)
                {
                    combined = new List<Int32>(first.Count + rules.Count);
                    combined.AddRange(first);
                }

                combined.AddRange(rules);
            }
        }

        #endregion

        #region Candidates

        /// <summary>
        /// The ascending indices of the candidates, i.e., the union of two ascending
        /// lists without common entries: a rule is either grouped or not.
        /// </summary>
        public readonly struct Candidates
        {
            private readonly List<Int32> _ungrouped;
            private readonly List<Int32>? _grouped;

            public Candidates(List<Int32> ungrouped, List<Int32>? grouped)
            {
                _ungrouped = ungrouped;
                _grouped = grouped;
            }

            public Int32 Count => _ungrouped.Count + (_grouped?.Count ?? 0);

            public Enumerator GetEnumerator() => new Enumerator(_ungrouped, _grouped);

            public struct Enumerator
            {
                private readonly List<Int32> _ungrouped;
                private readonly List<Int32>? _grouped;
                private Int32 _i;
                private Int32 _j;

                public Enumerator(List<Int32> ungrouped, List<Int32>? grouped)
                {
                    _ungrouped = ungrouped;
                    _grouped = grouped;
                    _i = 0;
                    _j = 0;
                    Current = -1;
                }

                public Int32 Current { get; private set; }

                public Boolean MoveNext()
                {
                    var hasUngrouped = _i < _ungrouped.Count;
                    var hasGrouped = _grouped is not null && _j < _grouped.Count;

                    if (hasUngrouped && (!hasGrouped || _ungrouped[_i] < _grouped![_j]))
                    {
                        Current = _ungrouped[_i++];
                        return true;
                    }
                    else if (hasGrouped)
                    {
                        Current = _grouped![_j++];
                        return true;
                    }

                    return false;
                }
            }
        }

        #endregion

        #region Groups

        /// <summary>
        /// Rules grouped by the id, class, or tag name their subject requires.
        /// </summary>
        private sealed class KeyedRules
        {
            // ids and classes match case-insensitively in quirks mode, tag names
            // in HTML documents; a superset of candidates is fine
            public Dictionary<String, List<Int32>> Ids { get; } = new(StringComparer.OrdinalIgnoreCase);

            public Dictionary<String, List<Int32>> Classes { get; } = new(StringComparer.OrdinalIgnoreCase);

            public Dictionary<String, List<Int32>> Tags { get; } = new(StringComparer.OrdinalIgnoreCase);

            public Dictionary<String, List<Int32>> Attributes { get; } = new(StringComparer.OrdinalIgnoreCase);

            // the rules for :root, i.e., the document element
            public List<Int32> Root { get; } = new();

            public List<Int32> Ungrouped { get; } = new();

            // the rules keyed by the parent or an ancestor of their subject (only the groups are used)
            public KeyedRules? ParentRules { get; private set; }

            public KeyedRules? AncestorRules { get; private set; }

            public void Add(SubjectKey[]? keys, Int32 index)
            {
                if (keys is null)
                {
                    Ungrouped.Add(index);
                    return;
                }

                foreach (var key in keys)
                {
                    if (key.Relation != KeyRelation.Self)
                    {
                        var context = key.Relation == KeyRelation.Parent ? ParentRules ??= new KeyedRules() : AncestorRules ??= new KeyedRules();
                        context.Add(new[] { new SubjectKey(key.Kind, key.Name) }, index);
                        continue;
                    }

                    if (key.Kind == SubjectKeyKind.Root)
                    {
                        if (Root.Count == 0 || Root[Root.Count - 1] != index)
                        {
                            Root.Add(index);
                        }

                        continue;
                    }

                    var map = key.Kind switch
                    {
                        SubjectKeyKind.Id => Ids,
                        SubjectKeyKind.Class => Classes,
                        SubjectKeyKind.Tag => Tags,
                        _ => Attributes,
                    };

                    if (!map.TryGetValue(key.Name, out var rules))
                    {
                        rules = new List<Int32>();
                        map.Add(key.Name, rules);
                    }

                    // the same key may appear more than once, e.g., ".a:hover, .a:focus"
                    if (rules.Count == 0 || rules[rules.Count - 1] != index)
                    {
                        rules.Add(index);
                    }
                }
            }
        }

        #endregion

        #region Key

        /// <summary>
        /// What the index needs to know about the selector of a rule. Collections
        /// (and hence indices) are created for every cascade, e.g., when computing
        /// the style of a single element, so this is kept with the rule.
        /// </summary>
        internal sealed class SelectorInfo
        {
            private SelectorInfo(ISelector? selector, ICssSelectorParser? parser)
            {
                Keys = SubjectKeyVisitor.GetKeys(selector, parser);
                TargetsPseudo = selector is not null && PseudoElementSubjectVisitor.TargetsPseudoElement(selector);
            }

            public SubjectKey[]? Keys { get; }

            public Boolean TargetsPseudo { get; }

            public static SelectorInfo Get(ICssStyleRule rule)
            {
                if (rule is CssStyleRule styleRule)
                {
                    return styleRule.IndexInfo ??= Create(rule);
                }

                return Create(rule);
            }

            // the parser of the rule is needed to look into :is() and the like
            private static SelectorInfo Create(ICssStyleRule rule) =>
                new(rule.Selector, rule.Owner?.Context?.GetService<ICssSelectorParser>());
        }

        /// <summary>
        /// The kinds of keys, ordered from the most to the least selective.
        /// </summary>
        internal enum SubjectKeyKind
        {
            Root,
            Id,
            Class,
            Tag,
            Attribute,
        }

        /// <summary>
        /// Which element has to have a key: the subject itself, or (if the subject requires
        /// none) its parent or one of its ancestors, as with .list > * or .prose *.
        /// </summary>
        internal enum KeyRelation
        {
            Self,
            Parent,
            Ancestor,
        }

        internal readonly struct SubjectKey
        {
            public SubjectKey(SubjectKeyKind kind, String name, KeyRelation relation = KeyRelation.Self)
            {
                Kind = kind;
                Name = name;
                Relation = relation;
            }

            public SubjectKeyKind Kind { get; }

            public String Name { get; }

            public KeyRelation Relation { get; }
        }

        #endregion

        #region Visitor

        /// <summary>
        /// Determines the keys of which a matching element needs to have at
        /// least one. No keys (null) means that any element could match.
        /// </summary>
        private sealed class SubjectKeyVisitor : ISelectorVisitor
        {
            // the functional pseudo-classes matching if any of their arguments matches
            private static readonly String[] MatchingFunctions = new[]
            {
                PseudoClassNames.Is,
                PseudoClassNames.Where,
                PseudoClassNames.Matches,
            };

            // How many :is() levels are looked into. Each one parses its arguments again, so a
            // deeply nested selector would cost quadratic time (and stack) to index; real ones
            // nest a level or two. Deeper, the rule just stays ungrouped, which is always correct.
            private const Int32 MaxArgumentDepth = 8;

            private readonly ISelector _selector;
            private readonly ICssSelectorParser? _parser;
            private readonly Int32 _depth;
            private SubjectKey[]? _keys;

            private SubjectKeyVisitor(ISelector selector, ICssSelectorParser? parser, Int32 depth)
            {
                _selector = selector;
                _parser = parser;
                _depth = depth;
            }

            public static SubjectKey[]? GetKeys(ISelector? selector, ICssSelectorParser? parser) =>
                GetKeys(selector, parser, 0);

            private static SubjectKey[]? GetKeys(ISelector? selector, ICssSelectorParser? parser, Int32 depth)
            {
                // a forced pseudo-class matches independent of its own selector, which the
                // root group considers (see Collect)
                if (selector is ForcingPseudoClassSelector forcing && forcing.Name.Is(PseudoClassNames.Root))
                {
                    return new[] { new SubjectKey(SubjectKeyKind.Root, PseudoClassNames.Root) };
                }
                else if (selector is null || selector is ForcingPseudoClassSelector)
                {
                    return null;
                }

                var visitor = new SubjectKeyVisitor(selector, parser, depth);
                selector.Accept(visitor);
                return visitor._keys;
            }

            void ISelectorVisitor.Id(String value) => Set(SubjectKeyKind.Id, value);

            void ISelectorVisitor.Class(String name) => Set(SubjectKeyKind.Class, name);

            void ISelectorVisitor.Type(String name)
            {
                // namespace prefixes, nesting (&), and the universal selector are reported as types, too
                if (_selector.Specificity == Priority.OneTag && name != "*" && name != "&" && name.IndexOf('|') == -1)
                {
                    Set(SubjectKeyKind.Tag, name);
                }
            }

            void ISelectorVisitor.Attribute(String name, String op, String? value)
            {
                // the name is serialized, i.e., a namespace (ns|name) or escapes would not match
                // the name of the attribute; [name!=value] also matches without the attribute
                if (RequiresAttribute(op) && name.IndexOf('|') == -1 && name.IndexOf('\\') == -1)
                {
                    Set(SubjectKeyKind.Attribute, name);
                }
            }

            void ISelectorVisitor.Child(String name, Int32 step, Int32 offset, ISelector selector)
            {
            }

            void ISelectorVisitor.PseudoClass(String name)
            {
                if (name.Is(PseudoClassNames.Root))
                {
                    Set(SubjectKeyKind.Root, name);
                    return;
                }

                // :is(.a .b, .c) only exposes its text, but an element matching it matches one
                // of the arguments, i.e., it requires one of their keys - just as a list does
                var arguments = _depth < MaxArgumentDepth ? GetArguments(name) : null;

                if (arguments is not null && _parser is not null)
                {
                    var selector = _parser.ParseSelector(arguments);

                    // the arguments form a forgiving list, which may have dropped arguments that
                    // are valid only where the rule is (e.g., nested); then the keys are incomplete
                    if (selector is not null && selector.Text.Equals(arguments, StringComparison.Ordinal))
                    {
                        _keys = GetKeys(selector, _parser, _depth + 1);
                    }
                }
            }

            void ISelectorVisitor.PseudoElement(String name)
            {
            }

            void ISelectorVisitor.List(IEnumerable<ISelector> selectors)
            {
                var keys = new List<SubjectKey>();

                foreach (var selector in selectors)
                {
                    var alternative = GetKeys(selector, _parser, _depth);

                    if (alternative is null)
                    {
                        return;
                    }

                    keys.AddRange(alternative);
                }

                _keys = keys.Count > 0 ? keys.ToArray() : null;
            }

            void ISelectorVisitor.Combinator(IEnumerable<ISelector> selectors, IEnumerable<String> symbols)
            {
                var compounds = selectors.ToList();
                _keys = GetKeys(compounds.LastOrDefault(), _parser, _depth);

                // a subject without a key, or just a common one (e.g., the a of .menu a, or the
                // [class] of .card [class*=icon]) is better found by the id or class of its context
                if (compounds.Count > 1 && (_keys is null || _keys.All(key => key.Kind >= SubjectKeyKind.Tag)) &&
                    GetContextKeys(compounds, symbols.ToList()) is { } context &&
                    (_keys is null || context.All(key => key.Kind < SubjectKeyKind.Tag)))
                {
                    _keys = context;
                }
            }

            /// <summary>
            /// Finds the keys of the closest compound left of the subject that matches its parent
            /// or one of its ancestors, e.g., .list in .list > :first-child. Going left, the
            /// position of the matched element relative to the subject is tracked: it is the subject
            /// or a sibling of it, its parent or a sibling of that, or an ancestor or a sibling of
            /// one. Only the parent and the ancestors are looked up for an element (see Collect).
            /// </summary>
            private SubjectKey[]? GetContextKeys(List<ISelector> compounds, List<String> symbols)
            {
                var position = Position.SelfOrSibling;

                for (var i = Math.Min(compounds.Count - 2, symbols.Count - 1); i >= 0; i--)
                {
                    position = Move(position, symbols[i]);

                    if (position == Position.Unknown)
                    {
                        return null;
                    }

                    var relation = position switch
                    {
                        Position.Parent => KeyRelation.Parent,
                        Position.Ancestor => KeyRelation.Ancestor,
                        _ => KeyRelation.Self,
                    };

                    // a shadow host is matched from its shadow tree, but it is no parent element
                    if (relation != KeyRelation.Self && !compounds[i].Text.StartsWith(":host", StringComparison.OrdinalIgnoreCase))
                    {
                        var keys = GetKeys(compounds[i], _parser, _depth);

                        if (keys is not null && keys.All(key => key.Relation == KeyRelation.Self))
                        {
                            return keys.Select(key => new SubjectKey(key.Kind, key.Name, relation)).ToArray();
                        }
                    }
                }

                return null;
            }

            private static Position Move(Position position, String combinator)
            {
                if (combinator == CombinatorSymbols.Child)
                {
                    return position == Position.SelfOrSibling ? Position.Parent : Position.Ancestor;
                }
                else if (combinator == CombinatorSymbols.Descendant)
                {
                    return Position.Ancestor;
                }
                else if (combinator == CombinatorSymbols.Adjacent || combinator == CombinatorSymbols.Sibling)
                {
                    return position switch
                    {
                        Position.SelfOrSibling => Position.SelfOrSibling,
                        Position.Parent or Position.ParentSibling => Position.ParentSibling,
                        _ => Position.AncestorSibling,
                    };
                }

                // e.g., a column or a shadow-piercing combinator
                return Position.Unknown;
            }

            private enum Position
            {
                Unknown,
                SelfOrSibling,
                Parent,
                ParentSibling,
                Ancestor,
                AncestorSibling,
            }

            void ISelectorVisitor.Many(IEnumerable<ISelector> selectors)
            {
                // all parts of a compound must match, so any key of a part is required
                foreach (var selector in selectors)
                {
                    var keys = GetKeys(selector, _parser, _depth);

                    if (keys is not null && (_keys is null || IsBetter(keys, _keys)))
                    {
                        _keys = keys;
                    }
                }
            }

            private static Boolean RequiresAttribute(String op) =>
                op.Length == 0 || op is "=" or "~=" or "|=" or "^=" or "$=" or "*=";

            private static String? GetArguments(String pseudoClass)
            {
                foreach (var function in MatchingFunctions)
                {
                    if (pseudoClass.Length > function.Length + 2 &&
                        pseudoClass.StartsWith(function, StringComparison.OrdinalIgnoreCase) &&
                        pseudoClass[function.Length] == '(' && pseudoClass[pseudoClass.Length - 1] == ')')
                    {
                        return pseudoClass.Substring(function.Length + 1, pseudoClass.Length - function.Length - 2);
                    }
                }

                return null;
            }

            private void Set(SubjectKeyKind kind, String name)
            {
                if (!String.IsNullOrEmpty(name))
                {
                    _keys = new[] { new SubjectKey(kind, name) };
                }
            }

            // a key of the element itself is more selective than one of an ancestor, and a single
            // key is more selective than a list of keys
            private static Boolean IsBetter(SubjectKey[] keys, SubjectKey[] current)
            {
                var own = keys.All(key => key.Relation == KeyRelation.Self);
                var currentOwn = current.All(key => key.Relation == KeyRelation.Self);

                if (own != currentOwn)
                {
                    return own;
                }

                return keys.Length < current.Length || (keys.Length == 1 && current.Length == 1 && keys[0].Kind < current[0].Kind);
            }
        }

        #endregion
    }
}
