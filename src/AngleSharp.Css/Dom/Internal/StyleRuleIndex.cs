namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Parser;
    using AngleSharp.Dom;
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
            }

            _keys.Add(keys, index);
        }

        private static Candidates Collect(IElement element, KeyedRules keyed)
        {
            // The ungrouped rules (e.g., [data-theme] or :root) are candidates for every
            // element, hence they are not copied, but merged with the grouped ones while
            // iterating. Only the (few) groups of the element are combined, if needed.
            List<Int32>? first = null;
            List<Int32>? combined = null;
            var id = element.Id;

            if (!String.IsNullOrEmpty(id) && keyed.Ids.TryGetValue(id!, out var rules))
            {
                AddGroup(ref first, ref combined, rules);
            }

            foreach (var name in element.ClassList)
            {
                if (keyed.Classes.TryGetValue(name, out rules))
                {
                    AddGroup(ref first, ref combined, rules);
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

            public List<Int32> Ungrouped { get; } = new();

            public void Add(SubjectKey[]? keys, Int32 index)
            {
                if (keys is null)
                {
                    Ungrouped.Add(index);
                    return;
                }

                foreach (var key in keys)
                {
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
            Id,
            Class,
            Tag,
            Attribute,
        }

        internal readonly struct SubjectKey
        {
            public SubjectKey(SubjectKeyKind kind, String name)
            {
                Kind = kind;
                Name = name;
            }

            public SubjectKeyKind Kind { get; }

            public String Name { get; }
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

            private readonly ISelector _selector;
            private readonly ICssSelectorParser? _parser;
            private SubjectKey[]? _keys;

            private SubjectKeyVisitor(ISelector selector, ICssSelectorParser? parser)
            {
                _selector = selector;
                _parser = parser;
            }

            public static SubjectKey[]? GetKeys(ISelector? selector, ICssSelectorParser? parser)
            {
                // a forced pseudo-class matches independent of its own selector
                if (selector is null || selector is ForcingPseudoClassSelector)
                {
                    return null;
                }

                var visitor = new SubjectKeyVisitor(selector, parser);
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
                // :is(.a .b, .c) only exposes its text, but an element matching it matches one
                // of the arguments, i.e., it requires one of their keys - just as a list does
                var arguments = GetArguments(name);

                if (arguments is not null && _parser is not null)
                {
                    var selector = _parser.ParseSelector(arguments);

                    // the arguments form a forgiving list, which may have dropped arguments that
                    // are valid only where the rule is (e.g., nested); then the keys are incomplete
                    if (selector is not null && selector.Text.Equals(arguments, StringComparison.Ordinal))
                    {
                        _keys = GetKeys(selector, _parser);
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
                    var alternative = GetKeys(selector, _parser);

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
                _keys = GetKeys(selectors.LastOrDefault(), _parser);
            }

            void ISelectorVisitor.Many(IEnumerable<ISelector> selectors)
            {
                // all parts of a compound must match, so any key of a part is required
                foreach (var selector in selectors)
                {
                    var keys = GetKeys(selector, _parser);

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

            // a single key is more selective than a list of keys
            private static Boolean IsBetter(SubjectKey[] keys, SubjectKey[] current) =>
                keys.Length < current.Length || (keys.Length == 1 && current.Length == 1 && keys[0].Kind < current[0].Kind);
        }

        #endregion
    }
}
