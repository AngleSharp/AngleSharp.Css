namespace AngleSharp.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a render device that also carries the user preferences,
    /// e.g., the preferred color scheme.
    /// </summary>
    public interface IRenderDevicePreferences
    {
        /// <summary>
        /// Gets the user preferences, keyed by the name of the media feature
        /// they answer, e.g., "prefers-color-scheme" mapped to "dark". A name
        /// that is not contained remains an unknown media feature, i.e., a
        /// query using it never matches.
        /// </summary>
        IReadOnlyDictionary<String, String> Preferences { get; }
    }
}
