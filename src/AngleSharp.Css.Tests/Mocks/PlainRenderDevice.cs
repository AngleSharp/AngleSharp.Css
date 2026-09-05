namespace AngleSharp.Css.Tests.Mocks
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// A render device that deliberately does not implement
    /// <see cref="IRenderDevicePreferences"/>, i.e., what an existing
    /// third-party implementation of <see cref="IRenderDevice"/> looks like.
    /// </summary>
    sealed class PlainRenderDevice : IRenderDevice
    {
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
