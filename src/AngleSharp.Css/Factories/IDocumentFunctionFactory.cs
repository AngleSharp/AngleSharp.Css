namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a factory to create document functions.
    /// </summary>
    public interface IDocumentFunctionFactory
    {
        /// <summary>
        /// Creates a new document function for the given name and argument.
        /// </summary>
        /// <param name="name">The name of the document function.</param>
        /// <param name="url">The url to handle.</param>
        /// <returns>The created document function, if any.</returns>
        IDocumentFunction Create(String name, String url);
    }
}
