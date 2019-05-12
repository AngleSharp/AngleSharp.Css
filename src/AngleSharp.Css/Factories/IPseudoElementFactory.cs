namespace AngleSharp.Css
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents a factory to create pseudo elements.
    /// </summary>
    public interface IPseudoElementFactory
    {
        /// <summary>
        /// Creates the pseudo element instance for the given pseudo type.
        /// </summary>
        /// <param name="host">The hosting element.</param>
        /// <param name="type">The type of pseudo element.</param>
        /// <returns>The pseudo element instance.</returns>
        IPseudoElement Create(IElement host, String type);
    }
}
