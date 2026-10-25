namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Text;
    using System;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// The presentational hints of HTML elements (HTML 15.3 - 15.4), i.e. attributes like the
    /// width of an img, which map to CSS declarations. These take part in the cascade like
    /// author rules with a specificity of zero, which precede all other author rules.
    /// </summary>
    static class PresentationalHints
    {
        /// <summary>
        /// Gets the declarations the attributes of the element map to, if any.
        /// </summary>
        public static String? GetDeclarations(IElement element)
        {
            if (element is not IHtmlElement || !element.HasAttributes())
            {
                return null;
            }

            var hints = new StringBuilder();

            switch (element.LocalName)
            {
                case "img":
                case "video":
                case "embed":
                case "iframe":
                case "object":
                    AddDimension(hints, element, "width", "width", ignoreZero: false);
                    AddDimension(hints, element, "height", "height", ignoreZero: false);
                    break;
                case "input" when element.GetAttribute("type").Isi("image"):
                    AddDimension(hints, element, "width", "width", ignoreZero: false);
                    AddDimension(hints, element, "height", "height", ignoreZero: false);
                    break;
                case "table":
                    AddDimension(hints, element, "width", "width", ignoreZero: true);
                    AddDimension(hints, element, "height", "height", ignoreZero: false);
                    AddColor(hints, element, "bgcolor", "background-color");
                    break;
                case "td":
                case "th":
                    AddDimension(hints, element, "width", "width", ignoreZero: true);
                    AddDimension(hints, element, "height", "height", ignoreZero: false);
                    AddColor(hints, element, "bgcolor", "background-color");
                    break;
                case "tr":
                    AddDimension(hints, element, "height", "height", ignoreZero: false);
                    AddColor(hints, element, "bgcolor", "background-color");
                    break;
                case "thead":
                case "tbody":
                case "tfoot":
                    AddColor(hints, element, "bgcolor", "background-color");
                    break;
                case "col":
                case "colgroup":
                case "hr":
                    AddDimension(hints, element, "width", "width", ignoreZero: false);
                    break;
                case "body":
                    AddColor(hints, element, "bgcolor", "background-color");
                    AddColor(hints, element, "text", "color");
                    break;
                case "font":
                    AddColor(hints, element, "color", "color");
                    break;
            }

            return hints.Length > 0 ? hints.ToString() : null;
        }

        private static Boolean HasAttributes(this IElement element) => element.Attributes.Length > 0;

        /// <summary>
        /// Maps a dimension value (e.g. "100" or "50%") to the property (HTML 2.4.4.4).
        /// </summary>
        private static void AddDimension(StringBuilder hints, IElement element, String attribute, String property, Boolean ignoreZero)
        {
            var value = element.GetAttribute(attribute);

            if (value is null)
            {
                return;
            }

            var index = 0;

            while (index < value.Length && value[index].IsSpaceCharacter())
            {
                index++;
            }

            var start = index;

            while (index < value.Length && (value[index].IsDigit() || value[index] == Symbols.Dot))
            {
                index++;
            }

            if (index == start || !Double.TryParse(value.Substring(start, index - start), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var number) || (ignoreZero && number == 0))
            {
                return;
            }

            var unit = index < value.Length && value[index] == Symbols.Percent ? "%" : "px";
            hints.Append(property).Append(": ").Append(number.ToString(CultureInfo.InvariantCulture)).Append(unit).Append(';');
        }

        /// <summary>
        /// Maps a color (e.g. "#ffffff" or "white") to the property.
        /// </summary>
        private static void AddColor(StringBuilder hints, IElement element, String attribute, String property)
        {
            var value = element.GetAttribute(attribute)?.Trim();

            if (value is { Length: > 0 } && value.IndexOfAny(new[] { ';', '{', '}', '!' }) < 0)
            {
                hints.Append(property).Append(": ").Append(value).Append(';');
            }
        }
    }
}
