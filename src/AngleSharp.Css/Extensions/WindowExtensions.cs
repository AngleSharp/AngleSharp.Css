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
        /// <param name="media">The query string.</param>
        /// <returns>The MediaQueryList instance.</returns>
        [DomName("matchMedia")]
        public static IMediaQueryList MatchMedia(this IWindow window, String mediaText)
        {
            var document = window.Document;
            var media = new MediaList(document.Context) { MediaText = mediaText };
            return new CssMediaQueryList(window, media);
        }

        [DomName("getPseudoElements")]
        public static ICssPseudoElementList GetPseudoElements(this IWindow window, IElement element, String type)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gives the values of all the CSS properties of an element after
        /// applying the active stylesheets and resolving any basic computation
        /// those values may contain.
        /// </summary>
        /// <param name="element">The element to compute the style for.</param>
        /// <param name="pseudo">The optional pseudo selector to use.</param>
        /// <returns>The style declaration describing the element.</returns>
        [DomName("getComputedStyle")]
        public static ICssStyleDeclaration GetComputedStyle(this IWindow window, IElement element, String pseudo = null)
        {
            var styleCollection = window.GetStyleCollection();
            return styleCollection.ComputeDeclarations(element, pseudo);
        }

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

        [DomName("computeRawStyle")]
        public static ICssStyleDeclaration ComputeRawStyle(this IWindow window, IElement element)
        {
            // Computes the cascaded style first
            // Places current device info
            // Replaces the relative values with absolute ones
            // --> computed
            throw new NotImplementedException();
        }

        [DomName("computeUsedStyle")]
        public static ICssStyleDeclaration ComputeUsedStyle(this IWindow window, IElement element)
        {
            // Is this somewhere implemented ? I don't know what that should be.
            // --> used (?)
            throw new NotImplementedException();
        }
    }
}
