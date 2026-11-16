namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.Dom;

    /// <summary>
    /// Extends style rules with the typed map of their declarations.
    /// https://www.w3.org/TR/css-typed-om-1/#declared-stylepropertymap-objects
    /// </summary>
    [DomExposed("CSSStyleRule")]
    public static class StyleRuleStyleMapExtensions
    {
        /// <summary>
        /// Gets the typed map of the rule's declarations.
        /// </summary>
        /// <param name="rule">The style rule.</param>
        /// <returns>The map of the rule's declarations.</returns>
        [DomName("styleMap")]
        [DomAccessor(Accessors.Getter)]
        public static IStylePropertyMap GetStyleMap(this ICssStyleRule rule) => rule.Style.GetStyleMap();
    }
}
