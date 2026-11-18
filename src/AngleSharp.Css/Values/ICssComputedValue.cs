namespace AngleSharp.Css.Values
{
    using System;

    /// <summary>
    /// A value that may already be its own computed value, e.g., a color or a length in
    /// pixels. Computing such a value gives the value itself, i.e., it does not need to be
    /// computed for every element (which boxes a copy of a struct value, and creates the
    /// context to compute with and a declaration holding the copy).
    /// </summary>
    interface ICssComputedValue
    {
        /// <summary>
        /// Gets if computing the value gives the value itself, in any context.
        /// </summary>
        Boolean IsComputed { get; }
    }
}
