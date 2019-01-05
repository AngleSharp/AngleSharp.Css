namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Parser;
    using System;
    using System.IO;

    /// <summary>
    /// Represents a CSS @keyframe rule.
    /// </summary>
    sealed class CssKeyframeRule : CssRule, ICssKeyframeRule
    {
        #region Fields

        private readonly CssStyleDeclaration _style;
        private IKeyframeSelector _selector;

        #endregion

        #region ctor

        internal CssKeyframeRule(ICssStyleSheet owner)
            : base(owner, CssRuleType.Keyframe)
        {
            _style = new CssStyleDeclaration(this);
        }

        #endregion

        #region Properties

        public String KeyText
        {
            get { return _selector?.ToCss(); }
            set { _selector = KeyframeParser.Parse(value); }
        }

        public IKeyframeSelector Key
        {
            get { return _selector; }
        }

        ICssStyleDeclaration ICssKeyframeRule.Style
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
            var newRule = (ICssKeyframeRule)rule;
            _style.SetDeclarations(newRule.Style);
            _selector = newRule.Key;
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var block = _style.ToCssBlock(formatter);
            writer.Write(formatter.Rule(KeyText, null, block));
        }

        #endregion
    }
}
