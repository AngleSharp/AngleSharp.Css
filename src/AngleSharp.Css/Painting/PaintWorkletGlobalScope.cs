namespace AngleSharp.Css.Painting
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The global scope the modules of a paint worklet run in, which is what a scripting
    /// engine exposes to a module: registerPaint() and devicePixelRatio.
    /// https://www.w3.org/TR/css-paint-api-1/#paintworkletglobalscope
    /// </summary>
    [DomName("PaintWorkletGlobalScope")]
    public sealed class PaintWorkletGlobalScope
    {
        internal PaintWorkletGlobalScope(PaintWorklet worklet)
        {
            Worklet = worklet;
        }

        /// <summary>
        /// Gets the paint worklet the scope belongs to.
        /// </summary>
        public PaintWorklet Worklet { get; }

        /// <summary>
        /// Gets the document the paint worklet belongs to, e.g., to fetch modules with
        /// its browsing context.
        /// </summary>
        public IDocument Document => Worklet.Document;

        /// <summary>
        /// Gets the number of device pixels per CSS pixel, which is the resolution of the
        /// render device divided by 96 (or 1 without a render device).
        /// </summary>
        [DomName("devicePixelRatio")]
        public Double DevicePixelRatio => Worklet.DevicePixelRatio;

        /// <summary>
        /// Registers a paint definition under the given name, which is used by the paint()
        /// functions with this name from now on.
        /// </summary>
        /// <param name="name">The name of the paint definition.</param>
        /// <param name="definition">The paint definition.</param>
        /// <exception cref="ArgumentException">
        /// The TypeError for an empty name or a syntax of the input arguments that is not valid.
        /// </exception>
        /// <exception cref="DomException">
        /// An InvalidModificationError if the name has been registered already.
        /// </exception>
        [DomName("registerPaint")]
        public void RegisterPaint(String name, IPaintDefinition definition) => Worklet.RegisterPaint(name, definition);
    }
}
