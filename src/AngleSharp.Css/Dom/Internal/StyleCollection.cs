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
        private Int32 _defaultRuleCount;

        #endregion

        #region ctor

        public StyleCollection(IEnumerable<ICssStyleSheet> sheets, IRenderDevice device)
            : this(Enumerable.Empty<ICssStyleSheet>(), sheets, device)
        {
        }

        public StyleCollection(IEnumerable<ICssStyleSheet> defaultSheets, IEnumerable<ICssStyleSheet> sheets, IRenderDevice device)
        {
            _defaultSheets = defaultSheets;
            _sheets = sheets;
            _device = device;
        }

        #endregion

        #region Properties

        public IRenderDevice Device => _device;

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
            AddRules(rules, _defaultSheets);
            _defaultRuleCount = rules.Count;
            AddRules(rules, _sheets);
            return rules;
        }

        private void AddRules(List<ICssStyleRule> rules, IEnumerable<ICssStyleSheet> sheets)
        {
            foreach (var sheet in sheets)
            {
                if (!sheet.IsDisabled && sheet.Media.Validate(_device))
                {
                    foreach (var rule in sheet.Rules.GetMatchingStyles(_device))
                    {
                        rules.Add(rule);
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion
    }
}
