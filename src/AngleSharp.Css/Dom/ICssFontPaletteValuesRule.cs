namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents a @font-palette-values CSS rule.
    /// </summary>
    [DomName("CSSFontPaletteValuesRule")]
    public interface ICssFontPaletteValuesRule : ICssRule, ICssProperties
    {
        /// <summary>
        /// Gets the rule name.
        /// </summary>
        [DomName("name")]
        String Name { get; }
    }
}
