namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    sealed class StyleCollection : IStyleCollection
    {
        #region Fields

        private readonly IEnumerable<ICssStyleSheet> _sheets;
        private readonly IRenderDevice _device;
        private List<ICssStyleRule>? _rules;

        #endregion

        #region ctor

        public StyleCollection(IEnumerable<ICssStyleSheet> sheets, IRenderDevice device)
        {
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

        #endregion

        #region Helpers

        private List<ICssStyleRule> CollectRules()
        {
            var rules = new List<ICssStyleRule>();

            foreach (var sheet in _sheets)
            {
                if (!sheet.IsDisabled && sheet.Media.Validate(_device))
                {
                    foreach (var rule in sheet.Rules.GetMatchingStyles(_device))
                    {
                        rules.Add(rule);
                    }
                }
            }

            return rules;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion
    }
}
