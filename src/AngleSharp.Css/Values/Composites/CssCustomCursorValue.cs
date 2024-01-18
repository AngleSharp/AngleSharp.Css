namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    sealed class CssCustomCursorValue : ICssCompositeValue, IEquatable<CssCustomCursorValue>
    {
        #region Fields

        private readonly ICssImageValue _source;
        private readonly ICssValue _position;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new custom cursor value.
        /// </summary>
        /// <param name="source">The image source to display.</param>
        /// <param name="position">The position offset, if any.</param>
        public CssCustomCursorValue(ICssImageValue source, ICssValue position)
        {
            _source = source;
            _position = position;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the image to display as cursor.
        /// </summary>
        public ICssImageValue Source => _source;

        /// <summary>
        /// Gets the positional offset, if any.
        /// </summary>
        public ICssValue Position => _position;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var sb = StringBuilderPool.Obtain();

                sb.Append(_source.CssText);

                if (_position is not null)
                {
                    sb.Append(Symbols.Space);
                    sb.Append(_position.CssText);
                }

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
        public Boolean Equals(CssCustomCursorValue other)
        {
            return _position.Equals(other._position) && _source.Equals(other._source);
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssCustomCursorValue value && Equals(value);

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var source = (ICssImageValue)_source.Compute(context);
            var position = _position?.Compute(context);
            return new CssCustomCursorValue(source, position);
        }

        #endregion
    }
}
