#nullable disable
namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;
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
            var cascaded = style as CssStyleDeclaration;
            // The declarations inherited from the parent's computed style are already computed,
            // hence only the local ones are computed - the inherited ones are shared.
            var local = cascaded?.GetLocalDeclarations() ?? style;
            var localList = local as IReadOnlyList<ICssProperty>;
            var computedStyle = new CssStyleDeclaration(context.Context, localList?.Count ?? 0);
            var factory = context.Context.GetDeclarationFactory();
            var preservedShorthands = default(List<ICssProperty>);

            if (context is IFontComputeContext fonts)
            {
                computedStyle.RootFontSize = fonts.RootFontSize;
            }

            if (localList is not null)
            {
                // by index, as enumerating the list through its interface allocates (for every element)
                for (var i = 0; i < localList.Count; i++)
                {
                    ComputeLocal(localList[i]);
                }
            }
            else
            {
                foreach (var property in local)
                {
                    ComputeLocal(property);
                }
            }

            void ComputeLocal(ICssProperty property)
            {
                var computed = property.Compute(context);
                var substitutedKeyword = property.RawValue is not ICssSpecialValue && computed.RawValue is ICssSpecialValue;
                // the info is only needed for a value that may be a keyword
                var info = default(DeclarationInfo);
                // `unset` is `inherit` for an inherited property and `initial` otherwise
                var initialValue = computed.RawValue is CssInitialValue or CssUnsetValue ||
                    (CssProperty.MaySerializeAsKeyword(computed.RawValue) && (info = factory.Create(property.Name)).Shorthands.Length > 0 && computed.Value.Isi(CssKeywords.Initial));

                if ((computed.RawValue is null || substitutedKeyword || initialValue) && property.RawValue is not null && property is CssProperty cssProperty)
                {
                    var inherit = computed.RawValue is CssInheritValue ||
                        (computed.RawValue is not CssInitialValue && property.CanBeInherited);
                    var inherited = inherit && context is CssComputeContext cssContext ?
                        cssContext.InheritedValue(property.Name) : null;
                    var value = inherited ?? (info ?? factory.Create(property.Name)).ConvertInitialValue(cssProperty.Converter)?.Compute(context);
                    computed = new CssProperty(property.Name, cssProperty.Converter, cssProperty.Flags, value, property.IsImportant);
                }

                computedStyle.AddProperty(computed);

                // the flags of a declaration are the ones of its info
                var flags = property is CssProperty declaration ? declaration.Flags : factory.Create(property.Name).Flags;

                if ((flags & PropertyFlags.PreserveShorthand) != 0)
                {
                    (preservedShorthands ??= new List<ICssProperty>()).Add(property);
                }
            }

            if (preservedShorthands is null)
            {
                if (cascaded is not null)
                {
                    computedStyle.InheritLike(cascaded);
                }

                return computedStyle;
            }

            foreach (var shorthand in preservedShorthands)
            {
                var info = factory.Create(shorthand.Name);
                // the computed value, as the declared one may refer to custom properties (e.g.
                // `overflow: var(--overflow)`), which only the computed one has substituted
                var computedShorthand = computedStyle.GetDeclaration(shorthand.Name) ?? shorthand;
                var rawValue = computedShorthand.RawValue ?? info.Converter.Convert(new StringSource(computedShorthand.Value));
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
            var declarations = new CssStyleDeclaration(context.Context, style.Length);

            // Resolve local custom declarations before merging the parent. In
            // particular, initial must not disappear through IsInherited.
            foreach (var property in style)
            {
                declarations.AddProperty(property.Name.StartsWith("--", StringComparison.Ordinal) ? property.Compute(context) : property);
            }

            AddRegisteredProperties(declarations, style, context);
            declarations.InheritFrom(parent);
            LogicalProperties.Apply(declarations, context);
            return declarations;
        }

        /// <summary>
        /// Gives every registered custom property (@property) that is not declared its computed
        /// value, i.e., the initial value or the value of the parent (CSS Properties and Values
        /// API 2.4). Being an own declaration, the value also shadows the one of the parent for
        /// a property that is not inherited.
        /// </summary>
        private static void AddRegisteredProperties(CssStyleDeclaration declarations, ICssStyleDeclaration style, ICssComputeContext context)
        {
            var registry = (context as CssComputeContext)?.Registry;

            if (registry is null || registry.Count == 0)
            {
                return;
            }

            foreach (var registration in registry.Registrations)
            {
                if (style.GetProperty(registration.Name) is { RawValue: not null })
                {
                    continue;
                }

                var info = context.Context?.GetDeclarationInfo(registration.Name);
                var value = context.Resolve(registration.Name) ?? CssInvalidValue.Instance;
                declarations.AddProperty(new CssProperty(registration.Name, info?.Converter ?? ValueConverters.Any, info?.Flags ?? PropertyFlags.Inherited, value));
            }
        }

        /// <summary>
        /// Blockifies the computed display of a floated box, an absolutely positioned box,
        /// a flex or grid item and the root element, see CSS Display 3 §2.7 and CSS 2.1 §9.7.
        /// </summary>
        /// <param name="style">The computed style of the element.</param>
        /// <param name="element">The element the style was computed for.</param>
        /// <param name="parent">The computed style of the element's parent, if any.</param>
        /// <returns>The computed style, adjusted if needed.</returns>
        internal static ICssStyleDeclaration Blockify(this ICssStyleDeclaration style, IElement element, ICssStyleDeclaration parent = null)
        {
            if (style is CssStyleDeclaration declarations && (IsBlockified(declarations, element) || IsFlexOrGridItem(parent)))
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

        // the children of a flex or grid container are its items (CSS Flexbox §4, CSS Grid §6)
        private static Boolean IsFlexOrGridItem(ICssStyleDeclaration parent) =>
            parent is CssStyleDeclaration declarations &&
            declarations.GetDeclaration(PropertyNames.Display)?.RawValue is CssConstantValue<DisplayMode> mode &&
            mode.Value is DisplayMode.Flex or DisplayMode.InlineFlex or DisplayMode.Grid or DisplayMode.InlineGrid;

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
