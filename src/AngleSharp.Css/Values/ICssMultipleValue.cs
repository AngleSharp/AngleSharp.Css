namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a multiple CSS value holder.
    /// </summary>
    public interface ICssMultipleValue : ICssValue, IEnumerable<ICssValue>
    {
        /// <summary>
        /// Gets the number of values.
        /// </summary>
        Int32 Count { get; }

        /// <summary>
        /// Gets the value at the given index.
        /// </summary>
        /// <param name="index">The index of the value.</param>
        /// <returns>The associated value.</returns>
        ICssValue this[Int32 index] { get; }
    }
}
