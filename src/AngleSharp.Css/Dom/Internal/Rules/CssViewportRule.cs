namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// Represents the @viewport rule.
    /// </summary>
    sealed class CssViewportRule : CssDeclarationRule
    {
        internal CssViewportRule(ICssStyleSheet owner)
            : base(owner, CssRuleType.Viewport, RuleNames.ViewPort)
        {
        }

        protected override ICssProperty CreateNewProperty(String name)
        {
            return new CssFeatureProperty(name);
        }

        protected override void ReplaceWith(ICssRule rule)
        {
        }
    }
}
