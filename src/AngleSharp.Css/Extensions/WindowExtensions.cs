namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Extensions;
    using System;

    /// <summary>
    /// A set of useful extension methods for the Window class.
    /// </summary>
    [DomExposed("Window")]
    public static class WindowExtensions
    {
        /// <summary>
        /// Creates a new MediaQueryList object representing the parsed results
        /// of the specified media query string.
        /// </summary>
        /// <param name="window">The window to extend with the functionality.</param>
        /// <param name="mediaText">The query string.</param>
        /// <returns>The MediaQueryList instance.</returns>
        [DomName("matchMedia")]
        public static IMediaQueryList MatchMedia(this IWindow window, String mediaText)
        {
            var document = window.Document;
            var media = new MediaList(document.Context) { MediaText = mediaText };
            return new CssMediaQueryList(window, media);
        }

        /// <summary>
        /// Gets the pseudo elements of the given element by their type.
        /// </summary>
        /// <param name="window">The window to extend with the functionality.</param>
        /// <param name="element">The element to get the pseudo elements from.</param>
        /// <param name="type">The type of pseudo elements to get, if any.</param>
        /// <returns>The list of pseudo elements matching the query.</returns>
        [DomName("getPseudoElements")]
        public static ICssPseudoElementList GetPseudoElements(this IWindow window, IElement element, String type = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gives the values of all the CSS properties of an element after
        /// applying the active stylesheets and resolving any basic computation
        /// those values may contain.
        /// </summary>
        /// <param name="window">The window to extend with the functionality.</param>
        /// <param name="element">The element to compute the style for.</param>
        /// <param name="pseudo">The optional pseudo selector to use.</param>
        /// <returns>The style declaration describing the element.</returns>
        [DomName("getComputedStyle")]
        public static ICssStyleDeclaration GetComputedStyle(this IWindow window, IElement element, String pseudo = null)
        {
            var styleCollection = window.GetStyleCollection();
            return styleCollection.ComputeDeclarations(element, pseudo);
        }

        /// <summary>
        /// Computes the element's default style. This ignores any transitions or animations.
        /// Presentational hints such as bgColor are also ignored, as well as inline styles.
        /// </summary>
        /// <param name="window">The window to extend with the functionality.</param>
        /// <param name="element">The element to compute the style for.</param>
        /// <returns>The style declaration describing the element.</returns>
        [DomName("computeDefaultStyle")]
        public static ICssStyleDeclaration ComputeDefaultStyle(this IWindow window, IElement element)
        {
            // Ignores transitions and animations
            // Ignores author-level style
            // Ignores presentational hints (e.g. bgColor)
            // Ignores inline styles
            // --> computed
            throw new NotImplementedException();
        }

        /// <summary>
        /// Computes the element's raw style. This first computes the cascaded style and then
        /// replaces the relative values with the absolute ones taken from the current device
        /// info.
        /// </summary>
        /// <param name="window">The window to extend with the functionality.</param>
        /// <param name="element">The element to compute the style for.</param>
        /// <returns>The style declaration describing the element.</returns>
        [DomName("computeRawStyle")]
        public static ICssStyleDeclaration ComputeRawStyle(this IWindow window, IElement element)
        {
            // Computes the cascaded style first
            // Places current device info
            // Replaces the relative values with absolute ones
            // --> computed
            throw new NotImplementedException();
        }
    }
}
