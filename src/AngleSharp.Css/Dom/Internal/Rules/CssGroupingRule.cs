namespace AngleSharp.Css.Dom
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the CSSGroupingRule interface.
    /// </summary>
    abstract class CssGroupingRule : CssRule, ICssGroupingRule
    {
        #region Fields

        private readonly CssRuleList _rules;

        #endregion

        #region ctor

        internal CssGroupingRule(ICssStyleSheet owner, CssRuleType type)
            : base(owner, type)
        {
            _rules = new CssRuleList();
        }

        #endregion

        #region Properties

        public ICssRuleList Rules
        {
            get { return _rules; }
        }

        #endregion

        #region Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = (ICssGroupingRule)rule;
            ReplaceWith(newRule.Rules);
        }

        protected void ReplaceWith(IEnumerable<ICssRule> rules)
        {
            _rules.Clear();

            foreach (var rule in rules)
            {
                Add(rule);
            }
        }

        public Int32 Insert(String ruleText, Int32 index)
        {
            var rule = Parser.ParseRule(Owner, ruleText);
            _rules.Insert(index, rule);
            rule.SetParent(this);
            return index;    
        }

        public void RemoveAt(Int32 index)
        {
            if (index >= 0 && index < _rules.Length)
            {
                var rule = _rules[index];
                Remove(rule);
            }
        }

        public void Add(ICssRule rule)
        {
            _rules.Add(rule);
            rule.SetParent(this);
        }

        public void Remove(ICssRule rule)
        {
            _rules.Remove(rule);
            rule.SetParent(null);
        }

        #endregion
    }
}
