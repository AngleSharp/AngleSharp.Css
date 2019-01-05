namespace AngleSharp.Css.Values
{
    using System;

    /// <summary>
    /// The common interface for all CSS gradients.
    /// </summary>
    interface IGradient : IImageSource
    {
        /// <summary>
        /// Gets an enumeration of all stops.
        /// </summary>
        GradientStop[] Stops { get; }

        /// <summary>
        /// Gets if the gradient is repeating.
        /// </summary>
        Boolean IsRepeating { get; }
    }
}
