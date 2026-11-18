namespace AngleSharp.Css.Painting
{
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;
    using System.Globalization;

    /// <summary>
    /// Parses and serializes the colors of the canvas API.
    /// https://html.spec.whatwg.org/multipage/canvas.html#serialisation-of-a-color
    /// </summary>
    static class CanvasColor
    {
        public const String Black = "#000000";

        public const String TransparentBlack = "rgba(0, 0, 0, 0)";

        /// <summary>
        /// Parses the CSS color and gives its serialization, i.e., #rrggbb if it is
        /// opaque and rgba(r, g, b, a) otherwise.
        /// </summary>
        /// <returns>The serialization or null if the text is no color.</returns>
        public static String? Normalize(String? text)
        {
            if (text is null)
            {
                return null;
            }

            var source = new StringSource(text);
            source.SkipSpacesAndComments();
            var color = source.ParseColor();
            source.SkipSpacesAndComments();

            if (color is null || !source.IsDone)
            {
                return null;
            }

            var value = color.Value;

            if (value.A == 255)
            {
                return String.Concat("#", value.R.ToString("x2"), value.G.ToString("x2"), value.B.ToString("x2"));
            }

            return String.Format(CultureInfo.InvariantCulture, "rgba({0}, {1}, {2}, {3})", value.R, value.G, value.B, value.Alpha);
        }
    }
}
