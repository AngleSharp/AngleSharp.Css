#nullable disable
namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;
    using System.Linq;

    /// <summary>
    /// CSSOM API extension methods.
    /// </summary>
    public static class CssOmExtensions
    {
        /// <summary>
        /// Gets the computed style of the element.
        /// </summary>
        /// <param name="element">The element to compute the style for.</param>
        /// <param name="pseudo">The optional pseudo selector to use.</param>
        /// <returns>The computed style of the element.</returns>
        public static ICssStyleDeclaration ComputeStyle(this IElement element, String pseudo = null)
        {
            var window = element?.Owner?.DefaultView;
            return window?.GetComputedStyle(element, pseudo);
        }

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
        /// Computes the declarations using the given compute context.
        /// </summary>
        /// <param name="style">The base (raw) style.</param>
        /// <param name="context">The context to use for the calculation.</param>
        /// <returns>A new style declaration with the existing or computed values.</returns>
        public static ICssStyleDeclaration Compute(this ICssStyleDeclaration style, ICssComputeContext context)
        {
            var computedStyle = new CssStyleDeclaration(context.Context);

            foreach (var property in style)
            {
                var computed = property.Compute(context);

                var substitutedKeyword = property.RawValue is not ICssSpecialValue && computed.RawValue is ICssSpecialValue;

                if ((computed.RawValue is null || substitutedKeyword) && property.RawValue is not null && property is CssProperty cssProperty)
                {
                    var inherit = computed.RawValue is CssInheritValue ||
                        (computed.RawValue is not CssInitialValue && property.CanBeInherited);
                    var inherited = inherit && context is CssComputeContext cssContext ?
                        cssContext.InheritedValue(property.Name) : null;
                    var initial = context.Context.GetDeclarationInfo(property.Name).InitialValue;
                    var value = inherited ?? (initial is null ? null : cssProperty.Converter.Convert(initial.CssText)?.Compute(context));
                    computed = new CssProperty(property.Name, cssProperty.Converter, cssProperty.Flags, value, property.IsImportant);
                }

                computedStyle.AddProperty(computed);
            }

            return computedStyle;
        }

        internal static CssStyleDeclaration Cascade(this ICssStyleDeclaration style, ICssStyleDeclaration parent, ICssComputeContext context)
        {
            var declarations = new CssStyleDeclaration(context.Context);

            // Resolve local custom declarations before merging the parent. In
            // particular, initial must not disappear through IsInherited.
            foreach (var property in style)
            {
                declarations.AddProperty(property.Name.StartsWith("--", StringComparison.Ordinal) ? property.Compute(context) : property);
            }

            declarations.UpdateDeclarations(parent);
            return declarations;
        }
    }
}
