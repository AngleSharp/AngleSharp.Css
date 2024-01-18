namespace AngleSharp.Css.Values
{
    /// <summary>
    /// Defines the context for computing styles.
    /// </summary>
    public interface ICssComputeContext
    {
        /// <summary>
        /// Gets the device associated with the computation.
        /// </summary>
        IRenderDevice Device { get; }

        /// <summary>
        /// Gets the associated browsing context.
        /// </summary>
        IBrowsingContext Context { get; }
    }
}
