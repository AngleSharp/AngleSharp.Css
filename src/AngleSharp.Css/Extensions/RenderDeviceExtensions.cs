namespace AngleSharp.Css
{
    using System;

    /// <summary>
    /// Convenience methods for reading a render device's user preferences,
    /// such as the ones a DefaultRenderDevice is configured with.
    /// </summary>
    public static class RenderDeviceExtensions
    {
        /// <summary>
        /// Gets the value of the given user preference, or null if the device
        /// carries no preferences at all, or none for the given media feature.
        /// </summary>
        /// <param name="device">The render device to read, which may be null.</param>
        /// <param name="name">The media feature name, e.g., prefers-color-scheme.</param>
        /// <returns>The preference's keyword, or null if there is none.</returns>
        public static String? GetPreference(this IRenderDevice? device, String name)
        {
            if (device is IRenderDevicePreferences source)
            {
                var preferences = source.Preferences;

                if (preferences is not null && preferences.TryGetValue(name, out var value) && !String.IsNullOrEmpty(value))
                {
                    return value;
                }
            }

            return null;
        }
    }
}
