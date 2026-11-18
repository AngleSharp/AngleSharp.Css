namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;

    /// <summary>
    /// Extends elements with the typed map of their computed style.
    /// https://www.w3.org/TR/css-typed-om-1/#computed-stylepropertymapreadonly-objects
    /// </summary>
    [DomExposed("Element")]
    public static class ElementComputedStyleMapExtensions
    {
        /// <summary>
        /// Gets the typed map of the element's computed style, as script sees it. The map
        /// shows the computed style at the time of the call.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The map or null if the element is not in a document with a window.</returns>
        [DomName("computedStyleMap")]
        public static IStylePropertyMapReadOnly? ComputedStyleMap(this IElement element)
        {
            var window = element.Owner?.DefaultView;
            var style = window?.GetComputedStyleOfScript(element);
            return style is not null ? new StylePropertyMapReadOnly(style, element.Owner?.Context, true, element.Owner) : null;
        }
    }
}
