namespace AngleSharp.Css.Extensions
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A set of useful extension methods for the StyleCollection class.
    /// </summary>
    static class StyleCollectionExtensions
    {
        #region Methods

        /// <summary>
        /// Generates the style collection for the given window object.
        /// </summary>
        /// <param name="window">The window to host the style collection.</param>
        /// <returns>The device-bound style collection.</returns>
        public static StyleCollection GetStyleCollection(this IWindow window)
        {
            var document = window.Document;
            var device = document.Context.GetService<IRenderDevice>();
            var stylesheets = document.GetStyleSheets().OfType<ICssStyleSheet>();
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
        public static ICssStyleDeclaration ComputeDeclarations(this StyleCollection rules, IElement element, String pseudoSelector = null)
        {
            var computedStyle = new CssStyleDeclaration();

            if (!String.IsNullOrEmpty(pseudoSelector))
            {
                var pseudoElement = element?.Pseudo(pseudoSelector.TrimStart(':'));

                if (pseudoElement != null)
                {
                    element = pseudoElement;
                }
            }

            computedStyle.SetDeclarations(rules.ComputeCascadedStyle(element));
            var htmlElement = element as IHtmlElement;

            if (htmlElement != null)
            {
                computedStyle.SetDeclarations(htmlElement.GetStyle());
            }

            var nodes = element.GetAncestors().OfType<IElement>();

            foreach (var node in nodes)
            {
                var style = rules.ComputeCascadedStyle(node);
                computedStyle.UpdateDeclarations(style);
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
        /// <returns>Returns the cascaded read-only style declaration.</returns>
        public static ICssStyleDeclaration ComputeCascadedStyle(this StyleCollection styleCollection, IElement element)
        {
            var computedStyle = new CssStyleDeclaration();
            var rules = styleCollection.SortBySpecificity(element);

            foreach (var rule in rules)
            {
                var inlineStyle = rule.Style;
                computedStyle.SetDeclarations(inlineStyle);
            }

            return computedStyle;
        }

        #endregion

        #region Helpers

        private static IEnumerable<ICssStyleRule> SortBySpecificity(this IEnumerable<ICssStyleRule> rules, IElement element)
        {
            return rules.Where(m => m.Selector.Match(element)).OrderBy(m => m.Selector.Specificity);
        }

        #endregion
    }
}
