namespace AngleSharp.Css.Dom
{
    using AngleSharp.Text;
    using System;
    using System.IO;

    /// <summary>
    /// Represents an unknown CSS rule.
    /// </summary>
    sealed class CssUnknownRule : CssRule
    {
        #region Fields

        private readonly String _name;
        private readonly TextView _content;

        #endregion

        #region ctor

        public CssUnknownRule(ICssStyleSheet owner, String name, TextView content)
            : base(owner, CssRuleType.Unknown)
        {
            _name = name;
            _content = content;
        }

        #endregion

        #region Properties

        public String Name
        {
            get { return _name; }
        }

        public TextView Content
        {
            get { return _content; }
        }

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
