namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Parser;
    using AngleSharp.Dom;
    using System;
    using System.IO;

    /// <summary>
    /// Represents an @supports rule.
    /// </summary>
    sealed class CssSupportsRule : CssConditionRule, ICssSupportsRule
    {
        #region Fields
        
        private IConditionFunction _condition;

        #endregion

        #region ctor

        internal CssSupportsRule(ICssStyleSheet owner)
            : base(owner, CssRuleType.Supports)
        {
        }

        #endregion

        #region Properties

        public String ConditionText
        {
            get { return _condition.ToCss(); }
            set { SetConditionText(value, throwOnError: true); }
        }

        public IConditionFunction Condition
        {
            get { return _condition; }
        }

        #endregion

        #region Methods

        public Boolean SetConditionText(String value, Boolean throwOnError)
        {
            var condition = ConditionParser.Parse(value);
            
            if (condition == null)
            {
                if (throwOnError)
                    throw new DomException(DomError.Syntax);

                return false;
            }

            _condition = condition;
            return true;
        }

        protected override void ReplaceWith(ICssRule rule)
        {
            base.ReplaceWith(rule);
            var newRule = (ICssSupportsRule)rule;
            _condition = newRule.Condition;
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var rules = formatter.BlockRules(Rules);
            writer.Write(formatter.Rule(RuleNames.Supports, ConditionText, rules));
        }

        #endregion
    }
}
