namespace AngleSharp.Css.Dom
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a list of CSS stylesheets.
    /// </summary>
    public interface IStyleCollection : IEnumerable<ICssStyleRule>
    {
        /// <summary>
        /// Gets the associated render device.
        /// </summary>
        IRenderDevice Device { get; }
    }
}
