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
        private ISelector _selector;

        #endregion

        #region ctor

        internal CssStyleRule(ICssStyleSheet owner)
            : base(owner, CssRuleType.Style)
        {
            _style = new CssStyleDeclaration(this);
        }

        #endregion

        #region Properties

        public ISelector Selector
        {
            get { return _selector; }
        }

        public String SelectorText
        {
            get { return _selector?.Text; }
            set { _selector = ParseSelector(value); }
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
            _selector = newRule.Selector;
            _style.SetDeclarations(newRule.Style);
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var block = _style.ToCssBlock(formatter);
            writer.Write(formatter.Rule(SelectorText, null, block));
        }

        #endregion
	}
}
