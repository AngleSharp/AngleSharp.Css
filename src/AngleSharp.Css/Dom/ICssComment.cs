namespace AngleSharp.Css.Dom
{
    using System;

    /// <summary>
    /// Represents a comment in the CSSOM.
    /// </summary>
    public interface ICssComment : IStyleFormattable
    {
        /// <summary>
        /// Gets the contained comment data.
        /// </summary>
        String Data { get; }
    }
}
