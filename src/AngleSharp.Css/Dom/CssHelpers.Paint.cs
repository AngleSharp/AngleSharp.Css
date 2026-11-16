#nullable enable
namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.Painting;
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The paint worklet (CSS Painting API).
    /// https://www.w3.org/TR/css-paint-api-1/#dom-css-paintworklet
    /// </summary>
    public static partial class CssHelpers
    {
        /// <summary>
        /// Gets the paint worklet of the document of the window (CSS.paintWorklet), which
        /// holds the paint definitions used by the paint() functions of its styles.
        /// </summary>
        /// <param name="window">The hosting window.</param>
        /// <returns>The paint worklet, which is created when it is first used.</returns>
        [DomName("paintWorklet")]
        [DomAccessor(Accessors.Getter)]
        public static PaintWorklet GetPaintWorklet(this IWindow window)
        {
            if (window is null)
                throw new ArgumentNullException(nameof(window));

            return window.Document.GetPaintWorklet();
        }

        /// <summary>
        /// Gets the paint worklet of the document, which holds the paint definitions used
        /// by the paint() functions of its styles.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <returns>The paint worklet, which is created when it is first used.</returns>
        public static PaintWorklet GetPaintWorklet(this IDocument document)
        {
            if (document is null)
                throw new ArgumentNullException(nameof(document));

            return PaintWorklet.For(document);
        }
    }
}
