namespace AngleSharp.Css.Dom
{
    using System;

    /// <summary>
    /// Represents a value of a CSS property.
    /// </summary>
    public interface ICssValue
    {
        /// <summary>
        /// The text representation of the value.
        /// </summary>
        String CssText { get; }
    }
}
