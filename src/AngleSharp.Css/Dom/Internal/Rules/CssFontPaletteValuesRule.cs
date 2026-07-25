#nullable disable
namespace AngleSharp.Css.Dom
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Represents a CSS @font-palette-values rule.
    /// </summary>
    [DebuggerDisplay(null, Name = "CssFontPaletteValuesRule ({Name})")]
    sealed class CssFontPaletteValuesRule : CssDescriptorRule, ICssFontPaletteValuesRule
    {
        internal CssFontPaletteValuesRule(ICssStyleSheet owner)
            : base(owner, CssRuleType.FontPaletteValues, RuleNames.FontPaletteValues)
        {
        }

        public String Name { get; set; }

        protected override String PreludeText => Name;

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = (ICssFontPaletteValuesRule)rule;
            Name = newRule.Name;
            ReplaceWith((ICssProperties)newRule);
        }
    }
}
