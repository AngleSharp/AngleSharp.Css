#nullable disable
namespace AngleSharp.Css.Dom
{
    using System;
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// Represents a CSS @layer rule.
    /// </summary>
    [DebuggerDisplay(null, Name = "CssLayerRule ({Name})")]
    sealed class CssLayerRule : CssGroupingRule, ICssLayerRule
    {
        internal CssLayerRule(ICssStyleSheet owner)
            : base(owner, CssRuleType.Layer)
        {
        }

        public String Name { get; set; }

        public Boolean IsStatement { get; set; }

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = (ICssLayerRule)rule;
            Name = newRule.Name;
            IsStatement = newRule.IsStatement;
            base.ReplaceWith(rule);
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            if (IsStatement)
            {
                writer.Write(formatter.Rule(RuleNames.Layer, Name));
                return;
            }

            var rules = formatter.BlockRules(GetFormattableRules());
            writer.Write(formatter.Rule(RuleNames.Layer, Name, rules));
        }
    }
}
