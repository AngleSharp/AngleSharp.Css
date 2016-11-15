namespace AngleSharp.Css.Dom
{
    using System;
    using System.IO;

    /// <summary>
    /// Represents an @supports rule.
    /// </summary>
    sealed class CssSupportsRule : CssConditionRule, ICssSupportsRule
    {
        #region Fields

        private String _conditionText;
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
            get { return _conditionText; }
            set { _conditionText = value; _condition = null; }
        }

        public IConditionFunction Condition
        {
            get { return _condition ?? (_condition = Parser.ParseCondition(_conditionText)); }
            set { _condition = value; _conditionText = value.ToCss(); }
        }

        #endregion

        #region Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            base.ReplaceWith(rule);
            var newRule = (ICssSupportsRule)rule;
            _conditionText = newRule.ConditionText;
            _condition = newRule.Condition;
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var rules = formatter.Block(Rules);
            writer.Write(formatter.Rule("@supports", ConditionText, rules));
        }

        #endregion
    }
}
