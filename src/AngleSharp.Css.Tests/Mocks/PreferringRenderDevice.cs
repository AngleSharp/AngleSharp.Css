namespace AngleSharp.Css.Tests.Mocks
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A third-party render device that opts into the user preferences
    /// without deriving from <see cref="DefaultRenderDevice"/>.
    /// </summary>
    sealed class PreferringRenderDevice : IRenderDevice, IRenderDevicePreferences
    {
        private readonly Dictionary<String, String> _preferences;

        public PreferringRenderDevice(String name, String value)
        {
            _preferences = new Dictionary<String, String>(StringComparer.OrdinalIgnoreCase)
            {
                { name, value },
            };
        }

        public IReadOnlyDictionary<String, String> Preferences => _preferences;

        public DeviceCategory Category => DeviceCategory.Screen;

        public Int32 ColorBits => 32;

        public Int32 DeviceHeight => 800;

        public Int32 DeviceWidth => 1000;

        public Int32 Frequency => 60;

        public Boolean IsGrid => false;

        public Boolean IsInterlaced => false;

        public Boolean IsScripting => true;

        public Int32 MonochromeBits => 16;

        public Int32 Resolution => 96;

        public Int32 ViewPortHeight => 800;

        public Int32 ViewPortWidth => 1000;

        public Double RenderWidth => ViewPortWidth;

        public Double RenderHeight => ViewPortHeight;

        public Double FontSize => 16;
    }
}
