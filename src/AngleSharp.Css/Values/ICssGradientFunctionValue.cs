namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// The common interface for all CSS gradients.
    /// </summary>
    public interface ICssGradientFunctionValue : ICssImageValue
    {
        /// <summary>
        /// Gets an enumeration of all stops.
        /// </summary>
        ICssValue[] Stops { get; }

        /// <summary>
        /// Gets if the gradient is repeating.
        /// </summary>
        Boolean IsRepeating { get; }
    }
}
