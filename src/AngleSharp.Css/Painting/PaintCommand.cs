namespace AngleSharp.Css.Painting
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// A recorded operation of a rendering context or a path, named like the operation
    /// of the canvas API (e.g., fillRect, or fillStyle for setting the fill style), with
    /// the (validated and normalized) arguments it has been called with. Operations that
    /// were ignored, e.g., for non-finite arguments, are not recorded.
    /// </summary>
    public sealed class PaintCommand
    {
        internal PaintCommand(String name, params Object[] arguments)
        {
            Name = name;
            Arguments = arguments;
        }

        /// <summary>
        /// Gets the name of the operation, e.g., fillRect.
        /// </summary>
        public String Name { get; }

        /// <summary>
        /// Gets the arguments of the operation, e.g., four numbers for fillRect, a string,
        /// a gradient or a pattern for fillStyle, or a path for fill.
        /// </summary>
        public IReadOnlyList<Object> Arguments { get; }

        /// <summary>
        /// Gets the operation as it would be written in script, e.g., fillRect(0, 0, 10, 10).
        /// </summary>
        public override String ToString() =>
            $"{Name}({String.Join(", ", Arguments.Select(Format))})";

        private static String Format(Object argument) => argument switch
        {
            Double d => d.ToString("R", CultureInfo.InvariantCulture),
            String s => "\"" + s + "\"",
            Boolean b => b ? "true" : "false",
            Double[] list => "[" + String.Join(", ", list.Select(m => m.ToString("R", CultureInfo.InvariantCulture))) + "]",
            Enum e => "\"" + e.ToString().ToLowerInvariant() + "\"",
            null => "null",
            _ => argument.ToString() ?? String.Empty,
        };
    }
}
