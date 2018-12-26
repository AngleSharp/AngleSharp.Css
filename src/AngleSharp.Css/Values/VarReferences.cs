namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a CSS shorthand that includes var replacements.
    /// </summary>
    public class VarReferences : ICssValue
    {
        private readonly String _value;
        private readonly VarReference[] _references;

        /// <summary>
        /// Creates a new variable reference.
        /// </summary>
        /// <param name="value">The value of the shorthand property.</param>
        /// <param name="references">The included variable references.</param>
        public VarReferences(String value, IEnumerable<VarReference> references)
        {
            _value = value;
            _references = references.ToArray();
        }

        /// <summary>
        /// Gets the literal value of the shorthand.
        /// </summary>
        public String Value
        {
            get { return _value; }
        }

        /// <summary>
        /// Gets the references variables.
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
