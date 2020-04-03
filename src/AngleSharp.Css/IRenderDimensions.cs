namespace AngleSharp.Css
{
    using System;

    /// <summary>
    /// Represents the render settings for an element, used to calculate relative sizes (E.G. %, rem).
    /// </summary>
    public interface IRenderDimensions
    {
        /// <summary>
        /// Gets Width of the render box.
        /// </summary>
        Double RenderWidth { get; }

        /// <summary>
        /// Gets Height of the render box.
        /// </summary>
        Double RenderHeight { get; }

        /// <summary>
        /// Gets the default font size in px.
        /// </summary>
        Double FontSize { get; }
    }
}