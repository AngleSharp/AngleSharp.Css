namespace AngleSharp.Css.Dom
{
    using AngleSharp.Text;
    using System;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Represents an @keyframes rule.
    /// </summary>
    sealed class CssKeyframesRule : CssGroupingRule, ICssKeyframesRule
    {
        #region Fields

        private String _name;

        #endregion

        #region ctor

        internal CssKeyframesRule(ICssStyleSheet owner)
            : base(owner, CssRuleType.Keyframes)
        {
        }

        #endregion

        #region Properties

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        #endregion

        #region Methods

        public void Add(String ruleText)
        {
            var rule = Parser.ParseKeyframeRule(Owner, ruleText);
            Add(rule);
        }

        public void Remove(String key)
        {
            var element = Find(key);
            Remove(element);
        }

        public ICssKeyframeRule Find(String key)
        {
            return Rules.OfType<ICssKeyframeRule>().FirstOrDefault(m => key.Isi(m.KeyText));
        }

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = (ICssKeyframesRule)rule;
            _name = newRule.Name;
            ReplaceWith(newRule.Rules);
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var rules = formatter.BlockRules(Rules);
            writer.Write(formatter.Rule(RuleNames.Keyframes, _name, rules));
        }

        #endregion
    }
}
