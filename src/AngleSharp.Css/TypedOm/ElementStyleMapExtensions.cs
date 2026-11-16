namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;

    /// <summary>
    /// Extends elements with the typed map of their inline style.
    /// https://www.w3.org/TR/css-typed-om-1/#declared-stylepropertymap-objects
    /// </summary>
    [DomExposed("HTMLElement")]
    [DomExposed("SVGElement")]
    public static class ElementStyleMapExtensions
    {
        /// <summary>
        /// Gets the typed map of the element's inline style, i.e., of its style attribute.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The map or null if the element's context has no CSS support.</returns>
        [DomName("attributeStyleMap")]
        [DomAccessor(Accessors.Getter)]
        public static IStylePropertyMap? GetAttributeStyleMap(this IElement element) =>
            element.GetStyle()?.GetStyleMap(element.Owner);
    }
}
