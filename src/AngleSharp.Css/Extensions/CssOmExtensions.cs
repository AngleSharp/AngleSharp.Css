#nullable disable
namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
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
            var cascaded = style as CssStyleDeclaration;

            if (context is IFontComputeContext fonts)
            {
                computedStyle.RootFontSize = fonts.RootFontSize;
            }

            // The declarations inherited from the parent's computed style are already computed,
            // hence only the local ones are computed - the inherited ones are shared.
            var local = cascaded?.GetLocalDeclarations() ?? style;

            foreach (var property in local)
            {
                var computed = property.Compute(context);

                var substitutedKeyword = property.RawValue is not ICssSpecialValue && computed.RawValue is ICssSpecialValue;
                var info = context.Context.GetDeclarationInfo(property.Name);
                var initialValue = computed.RawValue is CssInitialValue ||
                    (info.Shorthands.Length > 0 && computed.Value.Isi(CssKeywords.Initial));

                if ((computed.RawValue is null || substitutedKeyword || initialValue) && property.RawValue is not null && property is CssProperty cssProperty)
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

            var factory = context.Context.GetDeclarationFactory();
            var preservedShorthands = local
                .Where(property => (factory.Create(property.Name).Flags & PropertyFlags.PreserveShorthand) != 0)
                .ToArray();

            foreach (var shorthand in preservedShorthands)
            {
                var info = factory.Create(shorthand.Name);
                var rawValue = shorthand.RawValue ?? info.Converter.Convert(new StringSource(shorthand.Value));
                var values = rawValue is null ? null : info.Expand(factory, rawValue);

                if (values is not null)
                {
                    for (var i = 0; i < info.Longhands.Length; i++)
                    {
                        var name = info.Longhands[i];

                        if (cascaded is not null ? cascaded.GetDeclaration(name) is not null : style.Any(property => property.Name.Is(name)))
                        {
                            continue;
                        }

                        var longhand = factory.Create(name);
                        computedStyle.AddProperty(new CssProperty(name, longhand.Converter, longhand.Flags, values[i], shorthand.IsImportant));
                    }
                }
            }

            if (cascaded is not null)
            {
                computedStyle.InheritLike(cascaded);
            }

            return computedStyle;
        }

        internal static CssStyleDeclaration PrepareComputedDeclarations(this ICssStyleDeclaration style, ICssStyleDeclaration parent, ICssComputeContext context)
        {
            var declarations = new CssStyleDeclaration(context.Context);

            // Resolve local custom declarations before merging the parent. In
            // particular, initial must not disappear through IsInherited.
            foreach (var property in style)
            {
                declarations.AddProperty(property.Name.StartsWith("--", StringComparison.Ordinal) ? property.Compute(context) : property);
            }

            declarations.InheritFrom(parent);
            LogicalProperties.Apply(declarations, context);
            return declarations;
        }

        /// <summary>
        /// Blockifies the computed display of a floated box, an absolutely positioned box
        /// and the root element, see CSS Display 3 §2.7 and CSS 2.1 §9.7.
        /// </summary>
        /// <param name="style">The computed style of the element.</param>
        /// <param name="element">The element the style was computed for.</param>
        /// <returns>The computed style, adjusted if needed.</returns>
        internal static ICssStyleDeclaration Blockify(this ICssStyleDeclaration style, IElement element)
        {
            if (style is CssStyleDeclaration declarations && IsBlockified(declarations, element))
            {
                // display has neither shorthands nor is it inherited, i.e., there is nothing to
                // derive: a missing declaration is the same as one without a value.
                var current = declarations.GetDeclaration(PropertyNames.Display);
                var value = Blockify(current?.RawValue);

                if (value is not null)
                {
                    var info = element.Owner.Context.GetDeclarationInfo(PropertyNames.Display);

                    // Replacing the own declaration avoids merging all the inherited declarations
                    // (e.g., thousands of custom properties on :root) into this style.
                    declarations.ReplaceDeclaration(new CssProperty(PropertyNames.Display, info.Converter, info.Flags, value, current?.IsImportant ?? false));
                }
            }

            return style;
        }

        private static Boolean IsBlockified(CssStyleDeclaration style, IElement element)
        {
            if (element is not IPseudoElement && element.Owner?.DocumentElement == element)
            {
                return true;
            }

            // neither has shorthands, i.e., nothing needs to be derived from other declarations
            var floating = style.GetDeclaration(PropertyNames.Float)?.Value;
            var position = style.GetDeclaration(PropertyNames.Position)?.Value ?? String.Empty;
            return (!String.IsNullOrEmpty(floating) && !floating.Isi(CssKeywords.None)) ||
                position.Isi(CssKeywords.Absolute) || position.Isi(CssKeywords.Fixed);
        }

        private static ICssValue Blockify(ICssValue display)
        {
            // A missing display property computes to its initial value, i.e., inline.
            if (display is null)
            {
                return CreateDisplay(CssKeywords.Block, DisplayMode.Block);
            }
            else if (display is CssConstantValue<DisplayMode> mode)
            {
                return Blockify(mode);
            }

            return null;
        }

        private static ICssValue Blockify(CssConstantValue<DisplayMode> mode)
        {
            switch (mode.Value)
            {
                case DisplayMode.Inline:
                case DisplayMode.InlineBlock:
                case DisplayMode.TableCaption:
                case DisplayMode.TableCell:
                case DisplayMode.TableColumn:
                case DisplayMode.TableColumnGroup:
                case DisplayMode.TableFooterGroup:
                case DisplayMode.TableHeaderGroup:
                case DisplayMode.TableRow:
                case DisplayMode.TableRowGroup:
                    return CreateDisplay(CssKeywords.Block, DisplayMode.Block);
                case DisplayMode.InlineTable:
                    return CreateDisplay(CssKeywords.Table, DisplayMode.Table);
                case DisplayMode.InlineFlex:
                    return CreateDisplay(CssKeywords.Flex, DisplayMode.Flex);
                case DisplayMode.InlineGrid:
                    return CreateDisplay(CssKeywords.Grid, DisplayMode.Grid);
                case DisplayMode.ListItem when mode.CssText.StartsWith(CssKeywords.Inline + " ", StringComparison.OrdinalIgnoreCase):
                    return CreateDisplay(mode.CssText.Substring(CssKeywords.Inline.Length + 1), DisplayMode.ListItem);
                case DisplayMode.Ruby when mode.CssText.Isi(CssKeywords.Ruby):
                    return CreateDisplay(String.Concat(CssKeywords.Block, " ", CssKeywords.Ruby), DisplayMode.Ruby);
                default:
                    return null;
            }
        }

        private static ICssValue CreateDisplay(String keyword, DisplayMode mode) => new CssConstantValue<DisplayMode>(keyword, mode);
    }
}
