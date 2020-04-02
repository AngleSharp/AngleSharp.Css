namespace AngleSharp.Css
{
    using System;

    /// <summary>
    /// Represents the default render device.
    /// </summary>
    public class DefaultRenderDevice : IRenderDevice
    {
        /// <inheritdoc />
        public DeviceCategory Category
        {
            get;
            set;
        } = DeviceCategory.Screen;

        /// <inheritdoc />
        public Int32 ColorBits
        {
            get;
            set;
        } = 32;

        /// <inheritdoc />
        public Int32 DeviceHeight
        {
            get;
            set;
        } = 0;

        /// <inheritdoc />
        public Int32 DeviceWidth
        {
            get;
            set;
        } = 0;

        /// <inheritdoc />
        public Int32 Frequency
        {
            get;
            set;
        } = 60;

        /// <inheritdoc />
        public Boolean IsGrid
        {
            get;
            set;
        } = false;

        /// <inheritdoc />
        public Boolean IsInterlaced
        {
            get;
            set;
        } = false;

        /// <inheritdoc />
        public Boolean IsScripting
        {
            get;
            set;
        } = true;

        /// <inheritdoc />
        public Int32 MonochromeBits
        {
            get;
            set;
        } = 16;

        /// <inheritdoc />
        public Int32 Resolution
        {
            get;
            set;
        } = 96;

        /// <inheritdoc />
        public Int32 ViewPortHeight
        {
            get;
            set;
        } = 0;

        /// <inheritdoc />
        public Int32 ViewPortWidth
        {
            get;
            set;
        } = 0;

        /// <inheritdoc />
        public Double RenderWidth => ViewPortWidth;

        /// <inheritdoc />
        public Double RenderHeight => ViewPortHeight;

        /// <inheritdoc />
        public double FontSize
        {
            get;
            set;
        } = 16;
    }
}