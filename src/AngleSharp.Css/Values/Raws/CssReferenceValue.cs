namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a CSS shorthand that includes var replacements.
    /// </summary>
    /// <remarks>
    /// Creates a new variable reference.
    /// </remarks>
    /// <param name="value">The value of the shorthand property.</param>
    /// <param name="references">The included variable references.</param>
    public sealed class CssReferenceValue(String value, IEnumerable<Tuple<TextRange, CssVarValue>> references) : ICssRawValue
    {
        #region Fields

        private readonly String _value = value;
        private readonly TextRange[] _ranges = references.Select(m => m.Item1).ToArray();
        private readonly CssVarValue[] _references = references.Select(m => m.Item2).ToArray();

        #endregion

        #region ctor

        #endregion

        #region Properties

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

        #endregion

        #region Methods

        ICssValue? ICssValue.Compute(ICssComputeContext context)
        {
            foreach (var reference in _references)
            {
                var result = reference.Compute(context);

                if (result is not null)
                {
                    return result;
                }
            }

            return null;
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue? other) => Object.ReferenceEquals(this, other);

        #endregion
    }
}
