#nullable disable
namespace AngleSharp.Css.Dom
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Represents a CSS @color-profile rule.
    /// </summary>
    [DebuggerDisplay(null, Name = "CssColorProfileRule ({Name})")]
    sealed class CssColorProfileRule : CssDescriptorRule, ICssColorProfileRule
    {
        internal CssColorProfileRule(ICssStyleSheet owner)
            : base(owner, CssRuleType.ColorProfile, RuleNames.ColorProfile)
        {
        }

        public String Name { get; set; }

        protected override String PreludeText => Name;

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = (ICssColorProfileRule)rule;
            Name = newRule.Name;
            ReplaceWith((ICssProperties)newRule);
        }
    }
}
