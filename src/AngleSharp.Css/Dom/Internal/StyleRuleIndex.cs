namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Groups the style rules of a collection by the id, class, or tag name
    /// that the subject (i.e., the rightmost compound) of their selector
    /// requires. An element can only be matched by the rules in the groups of
    /// its own id, classes, and tag name, plus the rules that could not be
    /// grouped. Without the index every rule would be matched against every
    /// element. Pseudo-elements only need the rules targeting pseudo-elements.
    /// </summary>
    sealed class StyleRuleIndex
    {
        #region Fields

        // ids and classes match case-insensitively in quirks mode, tag names
        // in HTML documents; a superset of candidates is fine
        private readonly Dictionary<String, List<Int32>> _ids = new(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<String, List<Int32>> _classes = new(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<String, List<Int32>> _tags = new(StringComparer.OrdinalIgnoreCase);
        private readonly List<Int32> _ungrouped = new();
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
        public IReadOnlyList<Int32> GetCandidates(IElement element)
        {
            if (element is IPseudoElement)
            {
                return _pseudo;
            }

            var candidates = new List<Int32>(_ungrouped);
            var id = element.Id;

            if (!String.IsNullOrEmpty(id) && _ids.TryGetValue(id!, out var rules))
            {
                candidates.AddRange(rules);
            }

            foreach (var name in element.ClassList)
            {
                if (_classes.TryGetValue(name, out rules))
                {
                    candidates.AddRange(rules);
                }
            }

            if (_tags.TryGetValue(element.LocalName, out rules))
            {
                candidates.AddRange(rules);
            }

            if (candidates.Count > _ungrouped.Count)
            {
                // a rule may be found via several keys, e.g., ".a, .b" for class="a b"
                candidates.Sort();
                var count = 0;

                for (var i = 0; i < candidates.Count; i++)
                {
                    if (count == 0 || candidates[count - 1] != candidates[i])
                    {
                        candidates[count++] = candidates[i];
                    }
                }

                candidates.RemoveRange(count, candidates.Count - count);
            }

            return candidates;
        }

        #endregion

        #region Helpers

        private void Add(ICssStyleRule rule, Int32 index)
        {
            // nested rules are matched independently of their parent's selector
            var hasNested = rule.Rules.Length > 0;
            var info = SelectorInfo.Get(rule);
            var keys = hasNested ? null : info.Keys;

            if (hasNested || info.TargetsPseudo)
            {
                _pseudo.Add(index);
            }

            if (keys is null)
            {
                _ungrouped.Add(index);
                return;
            }

            foreach (var key in keys)
            {
                var map = key.Kind switch
                {
                    SubjectKeyKind.Id => _ids,
                    SubjectKeyKind.Class => _classes,
                    _ => _tags,
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

        #endregion

        #region Key

        /// <summary>
        /// What the index needs to know about the selector of a rule. Collections
        /// (and hence indices) are created for every cascade, e.g., when computing
        /// the style of a single element, so this is kept with the rule.
        /// </summary>
        internal sealed class SelectorInfo
        {
            private SelectorInfo(ISelector? selector)
            {
                Keys = SubjectKeyVisitor.GetKeys(selector);
                TargetsPseudo = selector is not null && PseudoElementSubjectVisitor.TargetsPseudoElement(selector);
            }

            public SubjectKey[]? Keys { get; }

            public Boolean TargetsPseudo { get; }

            public static SelectorInfo Get(ICssStyleRule rule)
            {
                if (rule is CssStyleRule styleRule)
                {
                    return styleRule.IndexInfo ??= new SelectorInfo(rule.Selector);
                }

                return new SelectorInfo(rule.Selector);
            }
        }

        /// <summary>
        /// The kinds of keys, ordered from the most to the least selective.
        /// </summary>
        internal enum SubjectKeyKind
        {
            Id,
            Class,
            Tag,
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
            private readonly ISelector _selector;
            private SubjectKey[]? _keys;

            private SubjectKeyVisitor(ISelector selector)
            {
                _selector = selector;
            }

            public static SubjectKey[]? GetKeys(ISelector? selector)
            {
                // a forced pseudo-class matches independent of its own selector
                if (selector is null || selector is ForcingPseudoClassSelector)
                {
                    return null;
                }

                var visitor = new SubjectKeyVisitor(selector);
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
            }

            void ISelectorVisitor.Child(String name, Int32 step, Int32 offset, ISelector selector)
            {
            }

            void ISelectorVisitor.PseudoClass(String name)
            {
            }

            void ISelectorVisitor.PseudoElement(String name)
            {
            }

            void ISelectorVisitor.List(IEnumerable<ISelector> selectors)
            {
                var keys = new List<SubjectKey>();

                foreach (var selector in selectors)
                {
                    var alternative = GetKeys(selector);

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
                _keys = GetKeys(selectors.LastOrDefault());
            }

            void ISelectorVisitor.Many(IEnumerable<ISelector> selectors)
            {
                // all parts of a compound must match, so any key of a part is required
                foreach (var selector in selectors)
                {
                    var keys = GetKeys(selector);

                    if (keys is not null && (_keys is null || IsBetter(keys, _keys)))
                    {
                        _keys = keys;
                    }
                }
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
