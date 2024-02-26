namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

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

        /// <summary>
        /// Gets the currently associated value converter.
        /// </summary>
        IValueConverter Converter { get; }

        /// <summary>
        /// Resolves a CSS variable by its name.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <returns>The value of the variable or null if no such variable exists.</returns>
        ICssValue Resolve(String name);
    }
}
