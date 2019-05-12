namespace AngleSharp.Css.Values
{
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a CSS shorthand that includes var replacements.
    /// </summary>
    sealed class CssReferenceValue : ICssRawValue
    {
        private readonly String _value;
        private readonly TextRange[] _ranges;
        private readonly CssVarValue[] _references;

        /// <summary>
        /// Creates a new variable reference.
        /// </summary>
        /// <param name="value">The value of the shorthand property.</param>
        /// <param name="references">The included variable references.</param>
        public CssReferenceValue(String value, IEnumerable<Tuple<TextRange, CssVarValue>> references)
        {
            _value = value;
            _ranges = references.Select(m => m.Item1).ToArray();
            _references = references.Select(m => m.Item2).ToArray();
        }

        /// <summary>
        /// Gets the literal value of the shorthand.
        /// </summary>
        public String Value => _value;

        /// <summary>
        /// Gets the positions of the variable references.
        /// </summary>
        public TextRange[] Ranges => _ranges;

        /// <summary>
        /// Gets the referenced variables.
        /// </summary>
        public CssVarValue[] References => _references;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => _value;
    }
}
