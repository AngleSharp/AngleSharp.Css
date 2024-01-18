namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Svg.Dom;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A set of useful extension methods for the StyleCollection class.
    /// </summary>
    public static class StyleCollectionExtensions
    {
        #region Methods

        /// <summary>
        /// Generates the style collection for the given window object.
        /// </summary>
        /// <param name="window">The window to host the style collection.</param>
        /// <returns>The device-bound style collection.</returns>
        public static IStyleCollection GetStyleCollection(this IWindow window)
        {
            var document = window.Document;
            var ctx = document.Context;
            var device = ctx.GetService<IRenderDevice>();
            var defaultStyleSheetProvider = ctx.GetServices<ICssDefaultStyleSheetProvider>();
            var defaultSheets = defaultStyleSheetProvider.Select(m => m.Default).Where(m => m != null);
            var currentSheets = document.GetStyleSheets().OfType<ICssStyleSheet>();
            var stylesheets = defaultSheets.Concat(currentSheets);
            return new StyleCollection(stylesheets, device);
        }

        /// <summary>
        /// Computes the declarations for the given element in the context of
        /// the specified styling rules.
        /// </summary>
        /// <param name="styles">The styles to use.</param>
        /// <param name="element">The element that is questioned.</param>
        /// <param name="pseudoSelector">The optional pseudo selector to use.</param>
        /// <returns>The style declaration containing all the declarations.</returns>
        public static ICssStyleDeclaration ComputeDeclarations(this IStyleCollection styles, IElement element, String pseudoSelector = null)
        {
            var ctx = element.Owner?.Context;
            var computedStyle = new CssStyleDeclaration(ctx);
            var context = new CssComputeContext(styles.Device, ctx, computedStyle);
            var nodes = element.GetAncestors().OfType<IElement>();

            if (!String.IsNullOrEmpty(pseudoSelector))
            {
                var pseudoElement = element?.Pseudo(pseudoSelector.TrimStart(':'));

                if (pseudoElement is not null)
                {
                    element = pseudoElement;
                }
            }

            computedStyle.SetDeclarations(styles.ComputeCascadedStyle(element));

            foreach (var node in nodes)
            {
                computedStyle.UpdateDeclarations(styles.ComputeCascadedStyle(node));
            }

            return computedStyle.Compute(context);
        }

        /// <summary>
        /// Computes the cascaded style, i.e. resolves the cascade by ordering after specificity.
        /// Two rules with the same specificity are ordered according to their appearance. The more
        /// recent declaration wins. Inheritance is not taken into account.
        /// </summary>
        /// <param name="styles">The style rules to apply.</param>
        /// <param name="element">The element to compute the cascade for.</param>
        /// <param name="parent">The potential parent for the cascade.</param>
        /// <returns>Returns the cascaded read-only style declaration.</returns>
        public static ICssStyleDeclaration ComputeCascadedStyle(this IStyleCollection styles, IElement element, ICssStyleDeclaration parent = null)
        {
            var ctx = element.Owner?.Context;
            var computedStyle = new CssStyleDeclaration(ctx);
            var rules = styles.SortBySpecificity(element);

            foreach (var rule in rules)
            {
                var inlineStyle = rule.Style;
                computedStyle.SetDeclarations(inlineStyle);
            }

            if (element is IHtmlElement || element is ISvgElement)
            {
                computedStyle.SetDeclarations(element.GetStyle());
            }

            if (parent is not null)
            {
                computedStyle.UpdateDeclarations(parent);
            }

            return computedStyle;
        }

        #endregion

        #region Helpers

        private static IEnumerable<ICssStyleRule> SortBySpecificity(this IEnumerable<ICssStyleRule> rules, IElement element)
        {
            IEnumerable<Tuple<ICssStyleRule, Priority>> MapPriority(ICssStyleRule rule)
            {
                if (rule.TryMatch(element, null, out var specificity))
                {
                    yield return Tuple.Create(rule, specificity);
                }

                foreach (var subRule in rule.Rules)
                {
                    if (subRule is ICssStyleRule style)
                    {
                        foreach (var item in MapPriority(style))
                        {
                            yield return item;
                        }
                    }
                }
            }

            return rules.SelectMany(MapPriority).OrderBy(GetPriority).Select(GetRule);
        }

        private static Priority GetPriority(Tuple<ICssStyleRule, Priority> item) => item.Item2;

        private static ICssStyleRule GetRule(Tuple<ICssStyleRule, Priority> item) => item.Item1;

        #endregion

        #region Context

        sealed class CssComputeContext : ICssComputeContext
        {
            private readonly IRenderDevice _device;
            private readonly IBrowsingContext _context;
            private readonly ICssProperties _properties;

            public CssComputeContext(IRenderDevice device, IBrowsingContext context, ICssProperties properties)
            {
                _device = device ?? new DefaultRenderDevice();
                _context = context;
                _properties = properties;
            }

            public IRenderDevice Device => _device;

            public IBrowsingContext Context => _context;

            public IValueConverter Converter => null;

            public ICssValue Resolve(String name)
            {
                if (name.StartsWith("--"))
                {
                    var property = _properties.FirstOrDefault(m => m.Name.Equals(name, StringComparison.Ordinal));
                    return property?.RawValue;
                }

                return null;
            }
        }

        #endregion
    }
}
