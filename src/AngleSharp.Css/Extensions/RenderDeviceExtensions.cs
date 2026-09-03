namespace AngleSharp.Css
{
    using System;

    static class RenderDeviceExtensions
    {
        /// <summary>
        /// Gets the value of the given user preference, or null if the device
        /// carries no preferences at all, or none for the given media feature.
        /// </summary>
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
