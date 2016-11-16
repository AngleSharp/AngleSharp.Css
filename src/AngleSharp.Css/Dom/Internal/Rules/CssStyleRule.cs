namespace AngleSharp.Css.Dom
{
    using System;
    using System.IO;

    /// <summary>
    /// Represents a CSS style rule.
    /// </summary>
	sealed class CssStyleRule : CssRule, ICssStyleRule
    {
        #region Fields

        private readonly CssStyleDeclaration _style;
        private String _selectorText;
        private ISelector _selector;

        #endregion

        #region ctor

        internal CssStyleRule(ICssStyleSheet owner)
            : base(owner, CssRuleType.Style)
        {
            _style = new CssStyleDeclaration(this);
            _selectorText = "*";
        }

        #endregion

        #region Properties

        public ISelector Selector
        {
            get { return _selector ?? (_selector = ParseSelector(_selectorText)); }
            set { _selector = value; _selectorText = value?.Text ?? "*"; }
        }

        public String SelectorText
        {
            get { return _selectorText; }
            set { _selectorText = value; _selector = null; }
        }

        ICssStyleDeclaration ICssStyleRule.Style
        {
            get { return _style; }
        }

        public CssStyleDeclaration Style
        {
            get { return _style; }
        }

        #endregion

        #region Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = (ICssStyleRule)rule;
            _selectorText = newRule.SelectorText;
            _selector = newRule.Selector;
            _style.SetDeclarations(newRule.Style);
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write(formatter.Style(_selectorText, _style));
        }

        #endregion
	}
}
