namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the @viewport rule.
    /// </summary>
    sealed class CssViewportRule : CssDeclarationRule
    {
        private static readonly HashSet<String> ContainedProperties = new HashSet<String>(StringComparer.OrdinalIgnoreCase)
        {
            PropertyNames.MinWidth,
            PropertyNames.MaxWidth,
            PropertyNames.Width,
            PropertyNames.MinHeight,
            PropertyNames.MaxHeight,
            PropertyNames.Height,
            PropertyNames.Zoom,
            PropertyNames.MinZoom,
            PropertyNames.MaxZoom,
            PropertyNames.UserZoom,
            PropertyNames.Orientation,
        };

        internal CssViewportRule(ICssStyleSheet owner)
            : base(owner, CssRuleType.Viewport, RuleNames.ViewPort, ContainedProperties)
        {
        }

        protected override void ReplaceWith(ICssRule rule)
        {
        }
    }
}
