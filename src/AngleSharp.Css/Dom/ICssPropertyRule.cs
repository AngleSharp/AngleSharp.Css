namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents a @property CSS rule.
    /// </summary>
    [DomName("CSSPropertyRule")]
    public interface ICssPropertyRule : ICssRule, ICssProperties
    {
        /// <summary>
        /// Gets the custom property name.
        /// </summary>
        [DomName("name")]
        String Name { get; }
    }
}
