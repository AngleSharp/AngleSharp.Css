namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a value that needs to be evaluated at runtime.
    /// </summary>
    public interface ICssRawValue : ICssValue
    {
        /// <summary>
        /// Gets the contained raw value.
        /// </summary>
        String Value { get; }
    }
}
