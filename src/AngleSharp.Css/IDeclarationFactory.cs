namespace AngleSharp.Css
{
    using System;

    /// <summary>
    /// Represents the factory the create CSS value converters.
    /// </summary>
    public interface IDeclarationFactory
    {
        /// <summary>
        /// Gets the declaration info for a property of the given name.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The associated declaration info.</returns>
        DeclarationInfo Create(String propertyName);
    }
}
