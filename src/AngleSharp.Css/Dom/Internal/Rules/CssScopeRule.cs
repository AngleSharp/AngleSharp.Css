#nullable disable
namespace AngleSharp.Css.Dom
{
    using System;
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// Represents a CSS @scope rule.
    /// </summary>
    [DebuggerDisplay(null, Name = "CssScopeRule ({ScopeText})")]
    sealed class CssScopeRule : CssGroupingRule, ICssScopeRule
    {
        internal CssScopeRule(ICssStyleSheet owner)
            : base(owner, CssRuleType.Scope)
        {
        }

        public String ScopeText { get; set; }

        protected override void ReplaceWith(ICssRule rule)
        {
            base.ReplaceWith(rule);
            ScopeText = ((ICssScopeRule)rule).ScopeText;
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var rules = formatter.BlockRules(GetFormattableRules());
            writer.Write(formatter.Rule(RuleNames.Scope, ScopeText, rules));
        }
    }
}
