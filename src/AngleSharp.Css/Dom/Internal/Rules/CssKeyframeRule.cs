namespace AngleSharp.Css.Dom
{
    using System;
    using System.IO;

    /// <summary>
    /// Represents a CSS @keyframe rule.
    /// </summary>
    sealed class CssKeyframeRule : CssRule, ICssKeyframeRule
    {
        #region Fields

        private readonly CssStyleDeclaration _style;
        private String _selectorText;
        private IKeyframeSelector _selector;

        #endregion

        #region ctor

        internal CssKeyframeRule(ICssStyleSheet owner)
            : base(owner, CssRuleType.Keyframe)
        {
            _style = new CssStyleDeclaration(owner.Context);
            _selectorText = CssKeywords.To;
            _style.SetParent(this);
        }

        #endregion

        #region Properties

        public String KeyText
        {
            get { return _selectorText; }
            set { _selectorText = value; _selector = null; }
        }

        public IKeyframeSelector Key
        {
            get { return _selector ?? (_selector = Parser.ParseKeyframeSelector(_selectorText)); }
            set { _selector = value; _selectorText = value.Text; }
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
            _selectorText = newRule.KeyText;
            _selector = newRule.Key;
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write(formatter.Style(_selectorText, _style));
        }

        #endregion
    }
}
