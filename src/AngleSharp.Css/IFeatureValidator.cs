namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a validator for media features.
    /// </summary>
    public interface IFeatureValidator
    {
        /// <summary>
        /// Validates the given media feature against the given device.
        /// </summary>
        /// <param name="feature">The feature to examine.</param>
        /// <param name="device">The device to use as basis.</param>
        /// <returns>True if the feature is present, otherwise false.</returns>
        Boolean Validate(IMediaFeature feature, IRenderDevice device);
    }
}
