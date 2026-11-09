#nullable enable
namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Css;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.RenderTree;
    using System;
    using System.Collections.Generic;
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
        public static ICssStyleDeclaration GetComputedStyle(this IWindow window, IElement element, String? pseudo = null)
        {
            var device = window.GetRenderDevice();
            var styleCollection = window.GetStyleCollection(device);
            return styleCollection.ComputeDeclarations(element, pseudo);
        }

        /// <summary>
        /// Gets the computed style as script sees it, where a property that was not declared has
        /// its initial value instead of being empty.
        /// </summary>
        /// <param name="window">The window to extend with the functionality.</param>
        /// <param name="element">The element to compute the style for.</param>
        /// <param name="pseudo">The optional pseudo selector to use.</param>
        /// <returns>The style declaration describing the element.</returns>
        [DomName("getComputedStyle")]
        public static ICssStyleDeclaration GetComputedStyleOfScript(this IWindow window, IElement element, String? pseudo = null)
        {
            var style = window.GetComputedStyle(element, pseudo);

            if (style is not CssStyleDeclaration declaration)
            {
                return style;
            }

            //  what is known from layout is what script gets to see (CSSOM 9), which a pseudo element has none of
            var provider = String.IsNullOrEmpty(pseudo) ? element.Owner?.Context.GetUsedValueProvider() : null;
            return declaration.WithInitialValues(provider != null ? name => provider.GetUsedValue(element, name) : null);
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

        /// <summary>
        /// Renders the currently available document into a render tree like
        /// <see cref="Render(IWindow, IRenderDevice)"/>, but with the given styles of the window
        /// (see <see cref="StyleCollectionExtensions.GetStyleCollection(IWindow, IRenderDevice)"/>)
        /// instead of gathering and indexing its rules anew, e.g. the ones that the styles of
        /// some elements were computed with already.
        /// </summary>
        /// <param name="window">The window to extend.</param>
        /// <param name="styles">The styles of the window to use.</param>
        /// <returns>The created render node.</returns>
        public static IRenderNode RenderDocument(this IWindow window, IStyleCollection styles)
        {
            var builder = RenderTreeBuilder.GetInstance(window);
            return builder.RenderDocument(styles);
        }

        /// <summary>
        /// Renders the element and its subtree again, below the render node of its parent from an
        /// earlier render tree, which stays as it is. This is for a change confined to the subtree
        /// (e.g. the inline style of the element), where rendering the whole document again would
        /// give the same nodes for everything else.
        ///
        /// In case no render device is supplied the context's default render device is chosen.
        /// </summary>
        /// <param name="window">The window to extend.</param>
        /// <param name="element">The element to render.</param>
        /// <param name="parent">The render node of the element's parent.</param>
        /// <param name="renderDevice">The device for rendering, if any.</param>
        /// <returns>The created render node, whose parent is the given one.</returns>
        public static ElementRenderNode Render(this IWindow window, IElement element, ElementRenderNode parent, IRenderDevice? renderDevice = null)
        {
            var builder = RenderTreeBuilder.GetInstance(window);
            return builder.RenderElement(element, parent, renderDevice ?? builder.DefaultDevice);
        }

        /// <summary>
        /// Renders the element below the render node of its parent like
        /// <see cref="Render(IWindow, IElement, ElementRenderNode, IRenderDevice)"/>, but with the given
        /// styles of the window (see <see cref="StyleCollectionExtensions.GetStyleCollection(IWindow, IRenderDevice)"/>)
        /// instead of gathering them anew, and the children of a node are only built when they
        /// are read, e.g. to find out if a change of state (like hovering) changes what is
        /// shown of an element without styling all of its subtree.
        /// </summary>
        /// <param name="window">The window to extend.</param>
        /// <param name="element">The element to render.</param>
        /// <param name="parent">The render node of the element's parent.</param>
        /// <param name="styles">The styles of the window to use.</param>
        /// <returns>The created render node, whose parent is the given one.</returns>
        public static ElementRenderNode RenderPartial(this IWindow window, IElement element, ElementRenderNode parent, IStyleCollection styles)
        {
            var builder = RenderTreeBuilder.GetInstance(window);
            return builder.RenderPartial(element, parent, styles);
        }

        /// <summary>
        /// Renders the currently available document into a render tree whose children are only
        /// built when they are read (like
        /// <see cref="RenderPartial(IWindow, IElement, ElementRenderNode, IStyleCollection)"/>), with
        /// the given styles of the window, e.g. to lay out just the part of a page that is in view
        /// without styling the rest. The given action gets the children of each node once they are
        /// built, before they are read, e.g. to complete them for the layout. The parts are built
        /// from the DOM and style sheets of the moment they are read.
        /// </summary>
        /// <param name="window">The window to extend.</param>
        /// <param name="styles">The styles of the window to use.</param>
        /// <param name="childrenBuilt">Gets a node and its children once they are built, if given.</param>
        /// <returns>The render node of the document element.</returns>
        public static ElementRenderNode RenderPartial(this IWindow window, IStyleCollection styles, Action<ElementRenderNode, IList<IRenderNode>>? childrenBuilt = null)
        {
            var builder = RenderTreeBuilder.GetInstance(window);
            return builder.RenderPartial(styles, childrenBuilt);
        }

        private static IRenderDevice GetRenderDevice(this IWindow window) =>
            window.Document.Context.GetService<IRenderDevice>() ?? new DefaultRenderDevice();
    }
}
