namespace AngleSharp.Css
{
    using System;

    /// <summary>
    /// Represents the factory to create media features.
    /// </summary>
    public interface IFeatureValidatorFactory
    {
        /// <summary>
        /// Creates a media feature validator for the given name.
        /// </summary>
        /// <param name="name">The name of the feature.</param>
        /// <returns>The media feature validator, if any.</returns>
        IFeatureValidator Create(String name);
    }
}
