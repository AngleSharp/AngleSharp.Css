namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents an image, e.g., url(a.png) or a gradient. The value is opaque, i.e.,
    /// it only knows its serialization.
    /// https://www.w3.org/TR/css-typed-om-1/#cssimagevalue
    /// </summary>
    [DomName("CSSImageValue")]
    public sealed class CssImageValue : CssStyleValue
    {
        private readonly String _text;

        internal CssImageValue(String text)
        {
            _text = text;
        }

        /// <summary>
        /// Serializes the image.
        /// </summary>
        public override String ToString() => _text;
    }
}
