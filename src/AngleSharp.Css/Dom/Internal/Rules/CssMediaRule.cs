namespace AngleSharp.Css.Dom
{
    using System;
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// Represents a CSS @media rule.
    /// </summary>
    [DebuggerDisplay(null, Name = "CssMediaRule ({ConditionText})")]
    sealed class CssMediaRule : CssConditionRule, ICssMediaRule
    {
        #region Fields

        private readonly MediaList _media;

        #endregion

        #region ctor

        internal CssMediaRule(ICssStyleSheet owner)
            : base(owner, CssRuleType.Media)
        {
            _media = new MediaList(owner.Context);
        }

        #endregion

        #region Properties

        public String ConditionText
        {
            get => _media.MediaText;
            set => _media.MediaText = value;
        }

        IMediaList ICssMediaRule.Media => _media;

        public MediaList Media => _media;

        #endregion

        #region Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            base.ReplaceWith(rule);
            var newRule = (ICssImportRule)rule;
            _media.Replace(newRule.Media);
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var rules = formatter.BlockRules(Rules);
            writer.Write(formatter.Rule(RuleNames.Media, ConditionText, rules));
        }

        #endregion
    }
}
