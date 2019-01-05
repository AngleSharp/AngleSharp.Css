namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a CSS line names definition.
    /// </summary>
    class LineNames : ICssValue
    {
        private readonly String[] _names;

        /// <summary>
        /// Creates a new line names definition.
        /// </summary>
        /// <param name="names">The names to contain.</param>
        public LineNames(IEnumerable<String> names)
        {
            _names = names.ToArray();
        }

        /// <summary>
        /// Gets the contained line names.
        /// </summary>
        public String[] Names
        {
            get { return _names; }
        }

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get { return $"[{String.Join(" ", _names)}]"; }
        }
    }
}
