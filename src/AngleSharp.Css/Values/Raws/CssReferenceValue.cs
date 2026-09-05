#nullable disable
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
    public sealed class CssReferenceValue : ICssRawValue
    {
        #region Fields

        private readonly String _value;
        private readonly TextRange[] _ranges;
        private readonly CssVarValue[] _references;
        private readonly CssVariableValue _tokens;
        private readonly CssVarValue[] _parsedReferences;

        #endregion

        #region ctor

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

        internal CssReferenceValue(CssVariableValue value, IEnumerable<Tuple<TextRange, CssVarValue>> references)
            : this(value.Text, references)
        {
            _parsedReferences = (CssVarValue[])_references.Clone();
            _tokens = value;
        }

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

        ICssValue ICssValue.Compute(ICssComputeContext context)
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

        internal ICssValue ComputeSubstituted(ICssComputeContext context)
        {
            // Direct value computation retains the public References contract.
            // Only unmodified parser-owned values use token-stream substitution
            // at the property computation boundary.
            if (HasCustomReferences)
            {
                return ((ICssValue)this).Compute(context);
            }

            var text = _tokens.Substitute(context.Resolve);
            return text is null ? null : ((ICssValue)new CssAnyValue(text)).Compute(context);
        }

        internal IEnumerable<CssVariableValue> GetVariableValues()
        {
            if (HasCustomReferences)
            {
                foreach (var reference in _references)
                {
                    yield return new CssVariableValue(reference.CssText);
                }
            }
            else
            {
                yield return _tokens;
            }
        }

        private Boolean HasCustomReferences
        {
            get
            {
                if (_tokens is null)
                {
                    return true;
                }

                for (var i = 0; i < _references.Length; i++)
                {
                    if (!Object.ReferenceEquals(_references[i], _parsedReferences[i]))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => Object.ReferenceEquals(this, other);

        #endregion
    }
}
