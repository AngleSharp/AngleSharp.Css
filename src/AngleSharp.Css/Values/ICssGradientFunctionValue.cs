namespace AngleSharp.Css.Values
{
    using System;

    /// <summary>
    /// The common interface for all CSS gradients.
    /// </summary>
    interface ICssGradientFunctionValue : ICssImageValue
    {
        /// <summary>
        /// Gets an enumeration of all stops.
        /// </summary>
        CssGradientStopValue[] Stops { get; }

        /// <summary>
        /// Gets if the gradient is repeating.
        /// </summary>
        Boolean IsRepeating { get; }
    }
}
