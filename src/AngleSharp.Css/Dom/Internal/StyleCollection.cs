namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System.Collections;
    using System.Collections.Generic;

    sealed class StyleCollection : IStyleCollection
    {
        #region Fields

        private readonly IEnumerable<ICssStyleSheet> _sheets;
        private readonly IRenderDevice _device;

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

        public IEnumerator<ICssStyleRule> GetEnumerator()
        {
            foreach (var sheet in _sheets)
            {
                if (!sheet.IsDisabled && sheet.Media.Validate(_device))
                {
                    var rules = sheet.Rules.GetMatchingStyles(_device);

                    foreach (var rule in rules)
                    {
                        yield return rule;
                    }
                }
            }
        }

        #endregion

        #region Helpers

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion
    }
}
