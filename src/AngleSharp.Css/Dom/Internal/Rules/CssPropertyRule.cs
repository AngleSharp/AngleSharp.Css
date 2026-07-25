#nullable disable
namespace AngleSharp.Css.Dom
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Represents a CSS @property rule.
    /// </summary>
    [DebuggerDisplay(null, Name = "CssPropertyRule ({Name})")]
    sealed class CssPropertyRule : CssDescriptorRule, ICssPropertyRule
    {
        internal CssPropertyRule(ICssStyleSheet owner)
            : base(owner, CssRuleType.Property, RuleNames.Property)
        {
        }

        public String Name { get; set; }

        protected override String PreludeText => Name;

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = (ICssPropertyRule)rule;
            Name = newRule.Name;
            ReplaceWith((ICssProperties)newRule);
        }
    }
}
