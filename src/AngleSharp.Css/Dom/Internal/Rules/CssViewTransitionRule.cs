#nullable disable
namespace AngleSharp.Css.Dom
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Represents a CSS @view-transition rule.
    /// </summary>
    [DebuggerDisplay(null, Name = "CssViewTransitionRule")]
    sealed class CssViewTransitionRule : CssDescriptorRule, ICssViewTransitionRule
    {
        internal CssViewTransitionRule(ICssStyleSheet owner)
            : base(owner, CssRuleType.ViewTransition, RuleNames.ViewTransition)
        {
        }

        protected override String PreludeText => null;

        protected override void ReplaceWith(ICssRule rule)
        {
            ReplaceWith((ICssProperties)rule);
        }
    }
}
