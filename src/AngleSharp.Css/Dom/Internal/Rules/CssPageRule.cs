namespace AngleSharp.Css.Dom
{
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
            set { _selector = ParseSelector(value); ; }
        }

        public ISelector Selector => _selector;

        ICssStyleDeclaration ICssPageRule.Style => _style;

        public CssStyleDeclaration Style => _style;

        #endregion

        #region Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = (ICssPageRule)rule;
            _selector = newRule.Selector;
            _style.SetDeclarations(newRule.Style);
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var rules = formatter.BlockRules(_style);
            writer.Write(formatter.Rule(RuleNames.Page, SelectorText, rules));
        }

        #endregion
    }
}
