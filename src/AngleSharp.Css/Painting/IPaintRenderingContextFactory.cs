namespace AngleSharp.Css.Painting
{
    using System;

    /// <summary>
    /// Creates the rendering contexts paint definitions draw with, e.g., one drawing into
    /// a bitmap of a graphics library. Register an implementation as a service of the
    /// configuration to replace the default one, which records the drawing operations
    /// (<see cref="RecordingPaintRenderingContext2D"/>).
    /// </summary>
    public interface IPaintRenderingContextFactory
    {
        /// <summary>
        /// Creates a new rendering context for drawing a single paint image.
        /// </summary>
        /// <param name="size">The size of the image in CSS pixels.</param>
        /// <param name="devicePixelRatio">
        /// The number of device pixels per CSS pixel, by which the output bitmap should be
        /// scaled for a crisp image.
        /// </param>
        /// <param name="settings">The options of the paint definition.</param>
        /// <returns>The rendering context, which is the result of drawing.</returns>
        IPaintRenderingContext2D Create(PaintSize size, Double devicePixelRatio, PaintRenderingContext2DSettings settings);
    }
}
