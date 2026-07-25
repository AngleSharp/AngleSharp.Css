#nullable disable
namespace AngleSharp.Css.Dom
{
    using System;
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// Represents a CSS @position-try rule.
    /// </summary>
    [DebuggerDisplay(null, Name = "CssPositionTryRule ({Name})")]
    sealed class CssPositionTryRule : CssRule, ICssPositionTryRule
    {
        private readonly CssStyleDeclaration _style;

        internal CssPositionTryRule(ICssStyleSheet owner)
            : base(owner, CssRuleType.PositionTry)
        {
            _style = new CssStyleDeclaration(this);
        }

        public String Name { get; set; }

        ICssStyleDeclaration ICssPositionTryRule.Style => _style;

        public CssStyleDeclaration Style => _style;

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = (ICssPositionTryRule)rule;
            Name = newRule.Name;
            _style.SetDeclarations(newRule.Style);
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var rules = _style.ToCssBlock(formatter);
            writer.Write(formatter.Rule(RuleNames.PositionTry, Name, rules));
        }
    }
}
