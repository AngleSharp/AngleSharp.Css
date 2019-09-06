namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;
    using System.Linq;

    /// <summary>
    /// CSSOM API extension methods.
    /// </summary>
    public static class CssOmExtensions
    {
        /// <summary>
        /// Gets the style rule with the provided selector text.
        /// </summary>
        /// <param name="rule">The container rule.</param>
        /// <param name="selectorText">The selector text to look for.</param>
        /// <returns>The style rule, if any.</returns>
        public static ICssStyleRule GetStyleRuleWith(this ICssGroupingRule rule, String selectorText) =>
            rule.Rules.GetStyleRuleWith(selectorText, rule.Owner?.Context);

        /// <summary>
        /// Gets the style rule with the provided selector text.
        /// </summary>
        /// <param name="sheet">The sheet.</param>
        /// <param name="selectorText">The selector text to look for.</param>
        /// <returns>The style rule, if any.</returns>
        public static ICssStyleRule GetStyleRuleWith(this ICssStyleSheet sheet, String selectorText) =>
            sheet.Rules.GetStyleRuleWith(selectorText, sheet.Context);

        /// <summary>
        /// Gets the style rule with the provided selector text.
        /// </summary>
        /// <param name="rules">The rules to look in.</param>
        /// <param name="selectorText">The selector text to look for.</param>
        /// <param name="context">The context for normalizing the CSS selector.</param>
        /// <returns>The style rule, if any.</returns>
        public static ICssStyleRule GetStyleRuleWith(this ICssRuleList rules, String selectorText, IBrowsingContext context = null)
        {
            var styleRules = rules.OfType<ICssStyleRule>();
            var parser = context?.GetService<ICssSelectorParser>();
            var normalizedSelectorText = parser?.ParseSelector(selectorText)?.Text ?? selectorText;

            foreach (var rule in styleRules)
            {
                if (rule.SelectorText.Is(normalizedSelectorText))
                {
                    return rule;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the ICssValue of a property with the given name.
        /// </summary>
        /// <param name="rule">The rule to extend.</param>
        /// <param name="propertyName">The property to obtain.</param>
        /// <returns>The value of the provided property, if any.</returns>
        public static ICssValue GetValueOf(this ICssStyleRule rule, String propertyName)
        {
            rule = rule ?? throw new ArgumentNullException(nameof(rule));
            var property = rule.Style.GetProperty(propertyName);
            return property?.RawValue;
        }

        /// <summary>
        /// Computes the values with knowledge of the device.
        /// </summary>
        /// <param name="style">The base (raw) style.</param>
        /// <param name="device">The device to use for the calculation.</param>
        /// <returns>A new style declaration with the existing or computed values.</returns>
        public static ICssStyleDeclaration Compute(this ICssStyleDeclaration style, IRenderDevice device)
        {
            //TODO
            return style;
        }
    }
}
