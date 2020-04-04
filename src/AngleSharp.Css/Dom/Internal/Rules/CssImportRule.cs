namespace AngleSharp.Css.Dom
{
    using System;
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// Represents a CSS import rule.
    /// </summary>
    [DebuggerDisplay(null, Name = "CssImportRule ({Href})")]
    sealed class CssImportRule : CssRule, ICssImportRule
    {
        #region Fields

        private readonly MediaList _media;
        private String _href;
        private ICssStyleSheet _styleSheet;

        #endregion

        #region ctor

        internal CssImportRule(ICssStyleSheet owner)
            : base(owner, CssRuleType.Import)
        {
            _media = new MediaList(owner.Context);
        }

        #endregion

        #region Properties

        public String Href
        {
            get => _href;
            set => _href = value;
        }

        IMediaList ICssImportRule.Media => _media;

        public MediaList Media => _media;

        public ICssStyleSheet Sheet
        {
            get => _styleSheet;
            set { _styleSheet = value; _styleSheet?.SetParent(Owner); }
        }

        #endregion

        #region Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = (ICssImportRule)rule;
            _media.Replace(newRule.Media);
            _href = newRule.Href;
            _styleSheet = newRule.Sheet;
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var media = Media.MediaText;
            var space = String.IsNullOrEmpty(media) ? String.Empty : " ";
            var value = String.Concat(_href.CssUrl(), space, media);
            writer.Write(formatter.Rule(RuleNames.Import, value));
        }

        #endregion
    }
}
