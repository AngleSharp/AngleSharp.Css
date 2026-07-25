#nullable disable
namespace AngleSharp.Css.Dom
{
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// Represents a CSS @starting-style rule.
    /// </summary>
    [DebuggerDisplay(null, Name = "CssStartingStyleRule")]
    sealed class CssStartingStyleRule : CssGroupingRule, ICssStartingStyleRule
    {
        internal CssStartingStyleRule(ICssStyleSheet owner)
            : base(owner, CssRuleType.StartingStyle)
        {
        }

        protected override void ReplaceWith(ICssRule rule)
        {
            base.ReplaceWith(rule);
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var rules = formatter.BlockRules(GetFormattableRules());
            writer.Write(formatter.Rule(RuleNames.StartingStyle, null, rules));
        }
    }
}
