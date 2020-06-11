namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
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
        /// <param name="rules">The styles to use.</param>
        /// <param name="element">The element that is questioned.</param>
        /// <param name="pseudoSelector">The optional pseudo selector to use.</param>
        /// <returns>The style declaration containing all the declarations.</returns>
        public static ICssStyleDeclaration ComputeDeclarations(this IEnumerable<ICssStyleRule> rules, IElement element, String pseudoSelector = null)
        {
            var computedStyle = new CssStyleDeclaration(element.Owner?.Context);
            var nodes = element.GetAncestors().OfType<IElement>();

            if (!String.IsNullOrEmpty(pseudoSelector))
            {
                var pseudoElement = element?.Pseudo(pseudoSelector.TrimStart(':'));

                if (pseudoElement != null)
                {
                    element = pseudoElement;
                }
            }

            computedStyle.SetDeclarations(rules.ComputeCascadedStyle(element));

            foreach (var node in nodes)
            {
                computedStyle.UpdateDeclarations(rules.ComputeCascadedStyle(node));
            }

            return computedStyle;
        }

        /// <summary>
        /// Computes the cascaded style, i.e. resolves the cascade by ordering after specifity.
        /// Two rules with the same specifity are ordered according to their appearance. The more
        /// recent declaration wins. Inheritance is not taken into account.
        /// </summary>
        /// <param name="styleCollection">The style rules to apply.</param>
        /// <param name="element">The element to compute the cascade for.</param>
        /// <param name="parent">The potential parent for the cascade.</param>
        /// <returns>Returns the cascaded read-only style declaration.</returns>
        public static ICssStyleDeclaration ComputeCascadedStyle(this IEnumerable<ICssStyleRule> styleCollection, IElement element, ICssStyleDeclaration parent = null)
        {
            var computedStyle = new CssStyleDeclaration(element.Owner?.Context);
            var rules = styleCollection.SortBySpecificity(element);

            foreach (var rule in rules)
            {
                var inlineStyle = rule.Style;
                computedStyle.SetDeclarations(inlineStyle);
            }

            if (element is IHtmlElement || element is ISvgElement)
            {
                computedStyle.SetDeclarations(element.GetStyle());
            }

            if (parent != null)
            {
                computedStyle.UpdateDeclarations(parent);
            }

            return computedStyle;
        }

        #endregion

        #region Helpers

        private static IEnumerable<ICssStyleRule> SortBySpecificity(this IEnumerable<ICssStyleRule> rules, IElement element) =>
            rules.Where(m => m.Selector?.Match(element) ?? false).OrderBy(m => m.Selector.Specificity);

        #endregion
    }
}
