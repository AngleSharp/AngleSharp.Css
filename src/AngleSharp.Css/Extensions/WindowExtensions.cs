#nullable enable
namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Css;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.RenderTree;
    using System;
    using System.Linq;

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
            var media = new MediaList(document.Context);
            // an invalid query does not throw, but becomes "not all" (CSSOM View, 4.2)
            media.SetMediaText(mediaText, throwOnError: false);
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
        public static ICssPseudoElementList GetPseudoElements(this IWindow window, IElement element, String? type = null)
        {
            var validTypes = new[] { "::before", "::after" };

            if (type == null)
            {
                // Everything is fine - we take all valid types
            }
            else if (validTypes.Contains(type))
            {
                validTypes = new[] { type };
            }
            else
            {
                throw new DomException(DomError.NotSupported);
            }

            return new CssPseudoElementList(validTypes.Select(pseudoSelector =>
            {
                var pseudoElement = element?.Pseudo(pseudoSelector.TrimStart(':'));

                if (pseudoElement is not null)
                {
                    var style = window.GetComputedStyle(pseudoElement);
                    return new CssPseudoElement(pseudoElement, pseudoSelector, style);
                }

                return null;
            }).OfType<CssPseudoElement>());
        }

        /// <summary>
        /// Gets the width of the viewport in CSS pixels, as given by the render device.
        /// </summary>
        /// <param name="window">The window to extend with the functionality.</param>
        /// <returns>The width of the viewport.</returns>
        [DomName("innerWidth")]
        [DomAccessor(Accessors.Getter)]
        public static Int32 InnerWidth(this IWindow window) => window.GetRenderDevice().ViewPortWidth;

        /// <summary>
        /// Gets the height of the viewport in CSS pixels, as given by the render device.
        /// </summary>
        /// <param name="window">The window to extend with the functionality.</param>
        /// <returns>The height of the viewport.</returns>
        [DomName("innerHeight")]
        [DomAccessor(Accessors.Getter)]
        public static Int32 InnerHeight(this IWindow window) => window.GetRenderDevice().ViewPortHeight;

        /// <summary>
        /// Gets the ratio of device pixels to CSS pixels, derived from the resolution of the
        /// render device (96 dpi being one device pixel per CSS pixel).
        /// </summary>
        /// <param name="window">The window to extend with the functionality.</param>
        /// <returns>The device pixel ratio.</returns>
        [DomName("devicePixelRatio")]
        [DomAccessor(Accessors.Getter)]
        public static Double DevicePixelRatio(this IWindow window)
        {
            var resolution = window.GetRenderDevice().Resolution;
            return resolution > 0 ? resolution / 96.0 : 1.0;
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
        public static ICssStyleDeclaration GetComputedStyle(this IWindow window, IElement element, String? pseudo = null)
        {
            var device = window.GetRenderDevice();
            var styleCollection = window.GetStyleCollection(device);
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

        /// <summary>
        /// Renders the currently available document into a render tree rooted at the returned render node.
        /// a render tree is essentially the combination of DOM nodes with their CSSOM computed style declarations.
        ///
        /// In case no render device is supplied the context's default render device is chosen.
        /// </summary>
        /// <param name="window">The window to extend.</param>
        /// <param name="renderDevice">The device for rendering, if any. </param>
        /// <returns>The created render node.</returns>
        public static IRenderNode Render(this IWindow window, IRenderDevice? renderDevice = null)
        {
            var builder = RenderTreeBuilder.GetInstance(window);
            return builder.RenderDocument(renderDevice);
        }

        private static IRenderDevice GetRenderDevice(this IWindow window) =>
            window.Document.Context.GetService<IRenderDevice>() ?? new DefaultRenderDevice();
    }
}
