namespace AngleSharp.Css.Dom
{
    using System;
    using AngleSharp.Css.Values;

    /// <summary>
    /// Represents a value of a CSS property.
    /// </summary>
    public interface ICssValue
    {
        /// <summary>
        /// The text representation of the value.
        /// </summary>
        String CssText { get; }

        /// <summary>
        /// Computes the current value using the given context.
        /// </summary>
        /// <param name="context">The used compute context.</param>
        /// <returns>The computed value or the original value, if already computed.</returns>
        ICssValue Compute(ICssComputeContext context);
    }
}
