namespace AngleSharp.Css.Dom
{
    using AngleSharp.Text;
    using System;
    using System.IO;

    /// <summary>
    /// Represents an unknown CSS rule.
    /// </summary>
    sealed class CssUnknownRule(ICssStyleSheet owner, String name, TextView content) : CssRule(owner, CssRuleType.Unknown)
    {
        #region Fields

        private readonly String _name = name;
        private readonly TextView _content = content;

        #endregion
        
        #region ctor

        #endregion

        #region Properties

        public String Name => _name;

        public TextView Content => _content;

        #endregion

        #region Methods

        protected override void ReplaceWith(ICssRule rule)
        {
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write(_content.Text);
        }

        #endregion
    }
}
