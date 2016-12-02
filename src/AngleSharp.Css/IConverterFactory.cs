namespace AngleSharp.Css
{
    using AngleSharp.Css.Converters;
    using System;

    /// <summary>
    /// Represents the factory the create CSS value converters.
    /// </summary>
    public interface IConverterFactory
    {
        /// <summary>
        /// Gets a value converter for a property of the given name.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The created value converter.</returns>
        IValueConverter Create(String propertyName);
    }
}
