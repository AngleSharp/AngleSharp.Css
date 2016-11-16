namespace AngleSharp.Css.Dom
{
    using AngleSharp.Text;
    using System;
    using System.IO;

    /// <summary>
    /// Represents the CSS @charset rule.
    /// </summary>
    sealed class CssCharsetRule : CssRule, ICssCharsetRule
    {
        #region Fields

        private String _charSet;

        #endregion

        #region ctor

        internal CssCharsetRule(ICssStyleSheet sheet)
            : base(sheet, CssRuleType.Charset)
        {
            _charSet = String.Empty;
        }

        #endregion

        #region Properties

        public String CharacterSet
        {
            get { return _charSet; }
            set { _charSet = value ?? String.Empty; }
        }

        #endregion

        #region Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            _charSet = ((ICssCharsetRule)rule).CharacterSet;
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write(formatter.Rule(RuleNames.Charset, _charSet.CssString()));
        }

        #endregion
    }
}
