namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Extensions;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a list of CSS stylesheets.
    /// </summary>
    sealed class StyleCollection : IEnumerable<ICssStyleRule>
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

        public IRenderDevice Device
        {
            get { return _device; }
        }

        #endregion

        #region Methods

        public IEnumerator<ICssStyleRule> GetEnumerator()
        {
            foreach (var sheet in _sheets)
            {
                if (!sheet.IsDisabled && sheet.Media.Validate(_device))
                {
                    var rules = GetRules(sheet.Rules);

                    foreach (var rule in rules)
                    {
                        yield return rule;
                    }
                }
            }
        }

        #endregion

        #region Helpers

        private IEnumerable<ICssStyleRule> GetRules(ICssRuleList rules)
        {
            foreach (var rule in rules)
            {
                if (rule.Type == CssRuleType.Media)
                {
                    var media = (ICssMediaRule)rule;

                    if (media.IsValid(_device))
                    {
                        var subrules = GetRules(media.Rules);

                        foreach (var subrule in subrules)
                        {
                            yield return subrule;
                        }
                    }
                }
                else if (rule.Type == CssRuleType.Supports)
                {
                    var support = (ICssSupportsRule)rule;

                    if (support.IsValid(_device))
                    {
                        var subrules = GetRules(support.Rules);

                        foreach (var subrule in subrules)
                        {
                            yield return subrule;
                        }
                    }
                }
                else if (rule.Type == CssRuleType.Style)
                {
                    yield return (ICssStyleRule)rule;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
