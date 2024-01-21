namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// Represents the @page rule.
    /// </summary>
    [DebuggerDisplay(null, Name = "CssPageRule ({SelectorText})")]
    sealed class CssPageRule : CssRule, ICssPageRule
    {
        #region Fields

        private readonly CssStyleDeclaration _style;
        private ISelector _selector;

        #endregion

        #region ctor

        internal CssPageRule(ICssStyleSheet owner)
            : base(owner, CssRuleType.Page)
        {
            _style = new CssStyleDeclaration(this);
        }

        #endregion

        #region Properties

        public String SelectorText
        {
            get => _selector?.Text;
            set { _selector = ParseSelector(value); }
        }

        public ISelector Selector => _selector;

        ICssStyleDeclaration ICssPageRule.Style => _style;

        public CssStyleDeclaration Style => _style;

        #endregion

        #region Methods

        internal void SetInvalidSelector(String selectorText)
        {
            _selector = new InvalidSelector(selectorText);
        }

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = (ICssPageRule)rule;
            _selector = newRule.Selector;
            _style.SetDeclarations(newRule.Style);
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var rules = _style.ToCssBlock(formatter);
            writer.Write(formatter.Rule(RuleNames.Page, SelectorText, rules));
        }

        #endregion

        #region Selector

        class InvalidSelector(String text) : ISelector
        {
            private readonly String _text = text;

            public String Text => _text;

            public Priority Specificity => Priority.Zero;

            public void Accept(ISelectorVisitor visitor)
            {
            }

            public Boolean Match(IElement element, IElement scope) => false;
        }

        #endregion
    }
}
