#nullable disable
namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Parser;
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// Represents a CSS @container rule.
    /// </summary>
    [DebuggerDisplay(null, Name = "CssContainerRule ({ConditionText})")]
    sealed class CssContainerRule : CssConditionRule, ICssContainerRule
    {
        #region Fields

        private String _containerName;
        private String _containerQuery;

        #endregion

        #region ctor

        internal CssContainerRule(ICssStyleSheet owner)
            : base(owner, CssRuleType.Container)
        {
        }

        #endregion

        #region Properties

        public String ConditionText
        {
            get
            {
                if (String.IsNullOrEmpty(_containerName))
                {
                    return _containerQuery ?? String.Empty;
                }

                if (String.IsNullOrEmpty(_containerQuery))
                {
                    return _containerName;
                }

                return String.Concat(_containerName, " ", _containerQuery);
            }
            set => SetConditionText(value, throwOnError: true);
        }

        public String ContainerName => _containerName ?? String.Empty;

        public String ContainerQuery => _containerQuery ?? String.Empty;

        #endregion

        #region Methods

        public Boolean SetConditionText(String value, Boolean throwOnError)
        {
            if (!TryParseCondition(value, out var name, out var query))
            {
                if (throwOnError)
                {
                    throw new DomException(DomError.Syntax);
                }

                return false;
            }

            _containerName = name;
            _containerQuery = query;
            return true;
        }

        protected override void ReplaceWith(ICssRule rule)
        {
            base.ReplaceWith(rule);
            var newRule = (ICssContainerRule)rule;
            _containerName = newRule.ContainerName;
            _containerQuery = newRule.ContainerQuery;
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var rules = formatter.BlockRules(GetFormattableRules());
            writer.Write(formatter.Rule(RuleNames.Container, ConditionText, rules));
        }

        private static Boolean TryParseCondition(String value, out String name, out String query)
        {
            name = String.Empty;
            query = String.Empty;
            var text = value?.Trim() ?? String.Empty;

            if (text.Length == 0)
            {
                return false;
            }

            var source = new StringSource(text);
            var candidateName = source.ParseCustomIdent();

            if (candidateName is not null)
            {
                source.SkipSpacesAndComments();

                if (!source.IsDone && !candidateName.Isi(CssKeywords.None))
                {
                    name = candidateName;
                    query = text.Substring(source.Index).Trim();
                    return query.Length > 0;
                }
            }

            query = text;
            return true;
        }

        #endregion
    }
}
