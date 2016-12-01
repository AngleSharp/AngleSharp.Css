namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Parser;
    using AngleSharp.Dom;
    using System;
    using System.IO;

    /// <summary>
    /// Contains the rules specified by a @document { /* ... */ } rule.
    /// </summary>
    sealed class CssDocumentRule : CssGroupingRule, ICssDocumentRule
    {
        #region Fields

        private readonly DocumentFunctions _conditions;

        #endregion

        #region ctor

        internal CssDocumentRule(ICssStyleSheet owner)
            : base(owner, CssRuleType.Document)
        {
            _conditions = new DocumentFunctions();
        }

        #endregion

        #region Properties

        public String ConditionText
        {
            get { return _conditions.ToCss(); }
            set { SetConditionText(value, throwOnError: true); }
        }

        public IDocumentFunctions Conditions
        {
            get { return _conditions; }
        }

        #endregion

        #region Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            base.ReplaceWith(rule);
            var newRule = (ICssDocumentRule)rule;
            _conditions.Clear();
            _conditions.AddRange(newRule.Conditions);
        }

        public Boolean SetConditionText(String value, Boolean throwOnError)
        {
            var factory = Owner.Context.GetService<IDocumentFunctionFactory>();
            var conditions = DocumentFunctionParser.Parse(value, factory);
            _conditions.Clear();

            if (conditions != null)
            {
                _conditions.AddRange(conditions);
                return true;
            }
            else if (throwOnError)
            {
                throw new DomException(DomError.Syntax);
            }

            return false;
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var rules = formatter.BlockRules(Rules);
            writer.Write(formatter.Rule(RuleNames.Document, ConditionText, rules));
        }

        #endregion
    }
}
