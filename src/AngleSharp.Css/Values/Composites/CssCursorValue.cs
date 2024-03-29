namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a CSS cursor definition.
    /// </summary>
    sealed class CssCursorValue : ICssCompositeValue, IEquatable<CssCursorValue>
    {
        #region Fields

        private readonly ICssValue[] _definitions;
        private readonly ICssValue _cursor;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS cursor definition.
        /// </summary>
        /// <param name="definitions">The different cursor definitions.</param>
        /// <param name="cursor">The system cursor to use.</param>
        public CssCursorValue(ICssValue[] definitions, ICssValue cursor)
        {
            _definitions = definitions;
            _cursor = cursor;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the custom cursor definitions.
        /// </summary>
        public ICssValue[] Definitions => _definitions;

        /// <summary>
        /// Gets the used system cursor.
        /// </summary>
        public ICssValue SystemCursor => _cursor;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var sb = StringBuilderPool.Obtain();

                foreach (var definition in _definitions)
                {
                    sb.Append(definition.CssText).Append(", ");
                }

                sb.Append(_cursor.CssText);
                return sb.ToPool();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssCursorValue other)
        {
            if (other is not null)
            {
                var comparer = EqualityComparer<ICssValue>.Default;
                var l = _definitions.Length;

                if (comparer.Equals(_cursor, other._cursor) && l == other._definitions.Length)
                {
                    for (var i = 0; i < l; i++)
                    {
                        var a = _definitions[i];
                        var b = other._definitions[i];

                        if (!comparer.Equals(a, b))
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssCursorValue value && Equals(value);

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var cursor = _cursor.Compute(context);
            var definitions = _definitions.Select(def => def.Compute(context)).ToArray();
            return new CssCursorValue(definitions, cursor);
        }

        #endregion
    }
}
