namespace AngleSharp.Css
{
    using System;

    /// <summary>
    /// Represents the renderers setting.
    /// </summary>
    public interface IRenderDevice
    {
        /// <summary>
        /// Gets the width of the viewport in pixels.
        /// </summary>
        Int32 ViewPortWidth { get; }

        /// <summary>
        /// Gets the height of the viewport in pixels.
        /// </summary>
        Int32 ViewPortHeight { get; }

        /// <summary>
        /// Gets if the output is interlaced.
        /// </summary>
        Boolean IsInterlaced { get; }

        /// <summary>
        /// Gets if scripting is supported.
        /// </summary>
        Boolean IsScripting { get; }

        /// <summary>
        /// Gets if the output is not a bitmap but a grid.
        /// </summary>
        Boolean IsGrid { get; }

        /// <summary>
        /// Gets the width of the device in pixels.
        /// </summary>
        Int32 DeviceWidth { get; }

        /// <summary>
        /// Gets the height of the device in pixels.
        /// </summary>
        Int32 DeviceHeight { get; }

        /// <summary>
        /// Gets the pixel density of the device in dpi.
        /// </summary>
        Int32 Resolution { get; }

        /// <summary>
        /// Gets the update frequency of the device in frames / s.
        /// </summary>
        Int32 Frequency { get; }

        /// <summary>
        /// Gets the number of color bits of the device, e.g. 32.
        /// </summary>
        Int32 ColorBits { get; }

        /// <summary>
        /// Gets the number of monochrome bits of the device, e.g. 0
        /// if the device is color.
        /// </summary>
        Int32 MonochromeBits { get; }

        /// <summary>
        /// Gets the category of the device.
        /// </summary>
        DeviceCategory Category { get; }

        /// <summary>
        /// Gets the associated browsing context.
        /// </summary>
        IBrowsingContext Context { get; }
    }
}
