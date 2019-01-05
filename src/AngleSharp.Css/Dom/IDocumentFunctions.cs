namespace AngleSharp.Css.Dom
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the aggregation of the document functions.
    /// </summary>
    public interface IDocumentFunctions : IEnumerable<IDocumentFunction>, IStyleFormattable
    {
        /// <summary>
        /// Gets the number of contained functions.
        /// </summary>
        Int32 Length { get; }

        /// <summary>
        /// Gets the function at the given index.
        /// </summary>
        /// <param name="index">The index of the function.</param>
        /// <returns>The function at the index.</returns>
        IDocumentFunction this[Int32 index] { get; }
    }
}
