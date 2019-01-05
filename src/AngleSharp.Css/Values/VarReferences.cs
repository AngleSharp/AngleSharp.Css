namespace AngleSharp.Css.Values
{
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a CSS shorthand that includes var replacements.
    /// </summary>
    class VarReferences : ICssRawValue
    {
        private readonly String _value;
        private readonly TextRange[] _ranges;
        private readonly VarReference[] _references;

        /// <summary>
        /// Creates a new variable reference.
        /// </summary>
        /// <param name="value">The value of the shorthand property.</param>
        /// <param name="references">The included variable references.</param>
        public VarReferences(String value, IEnumerable<Tuple<TextRange, VarReference>> references)
        {
            _value = value;
            _ranges = references.Select(m => m.Item1).ToArray();
            _references = references.Select(m => m.Item2).ToArray();
        }

        /// <summary>
        /// Gets the literal value of the shorthand.
        /// </summary>
        public String Value
        {
            get { return _value; }
        }

        /// <summary>
        /// Gets the positions of the variable references.
        /// </summary>
        public TextRange[] Ranges
        {
            get { return _ranges; }
        }

        /// <summary>
        /// Gets the referenced variables.
        /// </summary>
        public VarReference[] References
        {
            get { return _references; }
        }

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get { return _value; }
        }
    }
}
