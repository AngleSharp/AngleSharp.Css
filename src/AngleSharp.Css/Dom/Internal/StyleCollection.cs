namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    sealed class StyleCollection : IStyleCollection
    {
        #region Fields

        private readonly IEnumerable<ICssStyleSheet> _defaultSheets;
        private readonly IEnumerable<ICssStyleSheet> _sheets;
        private readonly IRenderDevice _device;
        private List<ICssStyleRule>? _rules;
        private List<Int32>? _layerRanks;
        private StyleRuleIndex? _index;
        private Int32 _defaultRuleCount;

        #endregion

        #region ctor

        public StyleCollection(IEnumerable<ICssStyleSheet> sheets, IRenderDevice device)
            : this(Enumerable.Empty<ICssStyleSheet>(), sheets, device)
        {
        }

        public StyleCollection(IEnumerable<ICssStyleSheet> defaultSheets, IEnumerable<ICssStyleSheet> sheets, IRenderDevice device, IElement? scope = null)
        {
            _defaultSheets = defaultSheets;
            _sheets = sheets;
            _device = device;
            Scope = scope;
        }

        #endregion

        #region Properties

        public IRenderDevice Device => _device;

        /// <summary>
        /// Gets the element the rules are scoped to, e.g. the host of a shadow tree whose style
        /// sheets these are (matched by :host), otherwise the document element.
        /// </summary>
        internal IElement? Scope { get; }

        #endregion

        #region Methods

        public IEnumerator<ICssStyleRule> GetEnumerator() => GetRules().GetEnumerator();

        /// <summary>
        /// Gets the flattened list of style rules that apply to the current
        /// device. The supplied sheet sequence is usually a lazy query over the
        /// document, so it is walked exactly once and the result is reused for
        /// the lifetime of the collection - which spans a single cascade or
        /// render pass. Without this the whole DOM would be re-walked for every
        /// element, and for every one of its ancestors.
        /// </summary>
        internal List<ICssStyleRule> GetRules() => _rules ??= CollectRules();

        /// <summary>
        /// Gets the index of the rules from GetRules, which narrows down the
        /// rules that need to be matched against an element.
        /// </summary>
        internal StyleRuleIndex GetIndex() => _index ??= new StyleRuleIndex(GetRules());

        /// <summary>
        /// Gets the rank of the cascade layer of the rule at the given index of
        /// GetRules: rules of a lower rank lose against rules of a higher rank
        /// (of the same origin). Unlayered rules have the highest rank.
        /// </summary>
        internal Int32 GetLayerRank(Int32 index)
        {
            GetRules();
            return _layerRanks![index];
        }

        /// <summary>
        /// Gets the number of rules at the beginning of the list from GetRules
        /// that stem from the default (user agent) stylesheets. These rules
        /// precede all author rules in the cascade, independent of specificity.
        /// </summary>
        internal Int32 GetDefaultRuleCount()
        {
            GetRules();
            return _defaultRuleCount;
        }

        #endregion

        #region Helpers

        private List<ICssStyleRule> CollectRules()
        {
            var rules = new List<ICssStyleRule>();
            var layers = new List<Layer>();
            AddRules(rules, layers, _defaultSheets);
            _defaultRuleCount = rules.Count;
            AddRules(rules, layers, WithoutEarlierDuplicates(_sheets));
            _layerRanks = layers.ConvertAll(m => m.Rank);
            return rules;
        }

        private void AddRules(List<ICssStyleRule> rules, List<Layer> layers, IEnumerable<ICssStyleSheet> sheets)
        {
            // each origin has its own layers
            var root = new Layer();

            foreach (var sheet in sheets)
            {
                if (!sheet.IsDisabled && sheet.Media.Validate(_device))
                {
                    AddRules(rules, layers, sheet.Rules, root);
                }
            }

            root.AssignRanks(0);
        }

        /// <summary>
        /// Leaves out a style sheet linked again later on (e.g. a page linking its style sheets
        /// twice). The later copy wins every tie the earlier one could, so the cascade stays the
        /// same, but every element would be matched against both. Sheets with cascade layers
        /// are kept, as layers are ordered by their first appearance.
        /// </summary>
        private static IEnumerable<ICssStyleSheet> WithoutEarlierDuplicates(IEnumerable<ICssStyleSheet> sheets)
        {
            var list = sheets as IReadOnlyList<ICssStyleSheet> ?? sheets.ToList();
            var duplicates = default(HashSet<Int32>);
            var seen = default(Dictionary<String, Int32>);

            for (var i = list.Count - 1; i >= 0; i--)
            {
                var sheet = list[i];
                var href = sheet.Href;

                if (String.IsNullOrEmpty(href) || HasLayers(sheet.Rules))
                {
                    continue;
                }

                var key = String.Concat(href, "\n", sheet.Media.MediaText, "\n", sheet.IsDisabled ? "1" : "0", "\n", sheet.Rules.Length.ToString());
                seen ??= new Dictionary<String, Int32>(StringComparer.Ordinal);

                if (seen.ContainsKey(key))
                {
                    (duplicates ??= new HashSet<Int32>()).Add(i);
                }
                else
                {
                    seen.Add(key, i);
                }
            }

            if (duplicates is null)
            {
                return list;
            }

            return list.Where((_, index) => !duplicates.Contains(index));
        }

        private static Boolean HasLayers(ICssRuleList rules)
        {
            foreach (var rule in rules)
            {
                if (rule is ICssLayerRule || (rule is ICssGroupingRule group && HasLayers(group.Rules)))
                {
                    return true;
                }
            }

            return false;
        }

        private void AddRules(List<ICssStyleRule> rules, List<Layer> layers, ICssRuleList list, Layer layer)
        {
            foreach (var rule in list)
            {
                switch (rule)
                {
                    case ICssStyleRule style:
                        rules.Add(style);
                        layers.Add(layer);
                        break;
                    case ICssMediaRule media when media.IsValid(_device):
                        AddRules(rules, layers, media.Rules, layer);
                        break;
                    case ICssSupportsRule supports when supports.IsValid(_device):
                        AddRules(rules, layers, supports.Rules, layer);
                        break;
                    case ICssLayerRule { IsStatement: true } statement:
                        // only declares the order of the layers, e.g. @layer reset, base;
                        foreach (var name in statement.Name.Split(','))
                        {
                            layer.Resolve(name);
                        }

                        break;
                    case ICssLayerRule block:
                        AddRules(rules, layers, block.Rules, String.IsNullOrWhiteSpace(block.Name) ? layer.AddAnonymous() : layer.Resolve(block.Name));
                        break;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        #region Layer

        /// <summary>
        /// A cascade layer (CSS Cascade 5, 6.4). Layers are ordered by their
        /// first appearance; the rules of a layer itself come after the ones of
        /// its sub-layers, and the unlayered rules after all layers.
        /// </summary>
        private sealed class Layer
        {
            private readonly Dictionary<String, Layer> _named = new(StringComparer.Ordinal);
            private readonly List<Layer> _children = new();

            public Int32 Rank { get; private set; }

            public Layer Resolve(String name)
            {
                var layer = this;

                // a.b is the sub-layer b of the layer a
                foreach (var part in name.Split('.'))
                {
                    var key = part.Trim();

                    if (!layer._named.TryGetValue(key, out var child))
                    {
                        child = new Layer();
                        layer._named.Add(key, child);
                        layer._children.Add(child);
                    }

                    layer = child;
                }

                return layer;
            }

            public Layer AddAnonymous()
            {
                var layer = new Layer();
                _children.Add(layer);
                return layer;
            }

            public Int32 AssignRanks(Int32 next)
            {
                foreach (var child in _children)
                {
                    next = child.AssignRanks(next);
                }

                Rank = next;
                return next + 1;
            }
        }

        #endregion
    }
}
