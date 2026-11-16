namespace AngleSharp.Css.Painting
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.TypedOm;
    using System;

    /// <summary>
    /// A pattern of an image, which can be used as a fill or stroke style.
    /// https://html.spec.whatwg.org/multipage/canvas.html#canvaspattern
    /// </summary>
    [DomName("CanvasPattern")]
    public sealed class CanvasPattern
    {
        internal CanvasPattern(CssImageValue image, String repetition)
        {
            Image = image;
            Repetition = repetition;
        }

        /// <summary>
        /// Gets the image of the pattern.
        /// </summary>
        public CssImageValue Image { get; }

        /// <summary>
        /// Gets how the image is repeated: repeat, repeat-x, repeat-y, or no-repeat.
        /// </summary>
        public String Repetition { get; }
    }
}
