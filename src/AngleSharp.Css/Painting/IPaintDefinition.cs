namespace AngleSharp.Css.Painting
{
    using AngleSharp.Css.TypedOm;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A paint definition, i.e., what registerPaint() is given: the class of a paint
    /// worklet, which draws the paint images of the paint() functions using its name.
    /// A scripting engine implements it for a class of a script, e.g., by constructing
    /// the class and calling its paint method, and an application may implement it
    /// directly in .NET (or use <see cref="PaintDefinition"/>).
    /// https://www.w3.org/TR/css-paint-api-1/#paint-definition
    /// </summary>
    public interface IPaintDefinition
    {
        /// <summary>
        /// Gets the properties (standard or custom) of the element the definition reads,
        /// i.e., the static inputProperties of the class. They are read once, when the
        /// definition is registered. Unknown properties are ignored.
        /// </summary>
        IEnumerable<String> InputProperties { get; }

        /// <summary>
        /// Gets the syntax (as for @property, e.g., &lt;length&gt; or &lt;color&gt;+) of
        /// each argument of the paint() function, i.e., the static inputArguments of the
        /// class. They are read once, when the definition is registered. If there are
        /// none, the arguments are given as they are written (CssUnparsedValue).
        /// </summary>
        IEnumerable<String> InputArguments { get; }

        /// <summary>
        /// Gets the options of the rendering context, i.e., the static contextOptions of
        /// the class. They are read once, when the definition is registered.
        /// </summary>
        PaintRenderingContext2DSettings ContextOptions { get; }

        /// <summary>
        /// Draws the paint image, i.e., calls the paint method of the class.
        /// </summary>
        /// <param name="context">The rendering context to draw with.</param>
        /// <param name="size">The size of the image.</param>
        /// <param name="properties">The input properties of the element.</param>
        /// <param name="arguments">The arguments of the paint() function.</param>
        void Paint(IPaintRenderingContext2D context, PaintSize size, IStylePropertyMapReadOnly properties, IReadOnlyList<CssStyleValue> arguments);
    }
}
