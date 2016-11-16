namespace AngleSharp.Css.Dom
{
    using System;
    using System.IO;

    /// <summary>
    /// Represents the @page rule.
    /// </summary>
    sealed class CssPageRule : CssRule, ICssPageRule
    {
        #region Fields

        private readonly CssStyleDeclaration _style;
        private String _selectorText;
        private ISelector _selector;

        #endregion

        #region ctor

        internal CssPageRule(ICssStyleSheet owner)
            : base(owner, CssRuleType.Page)
        {
            _style = new CssStyleDeclaration(owner.Context);
            _selectorText = "*";
            _style.SetParent(this);
        }

        #endregion

        #region Properties

        public String SelectorText
        {
            get { return _selectorText; }
            set { _selectorText = value; _selector = null; }
        }

        public ISelector Selector
        {
            get { return _selector ?? (_selector = ParseSelector(_selectorText)); }
            set { _selector = value; _selectorText = value.Text; }
        }

        ICssStyleDeclaration ICssPageRule.Style
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
            var newRule = (ICssPageRule)rule;
            _selectorText = newRule.SelectorText;
            _selector = newRule.Selector;
            _style.SetDeclarations(newRule.Style);
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var rules = formatter.Block(_style);
            writer.Write(formatter.Rule(RuleNames.Page, _selectorText, rules));
        }

        #endregion
    }
}
