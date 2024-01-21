namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents an unknown (any) value.
    /// </summary>
    /// <remarks>
    /// Creates a new unknown value with the given literal content.
    /// </remarks>
    /// <param name="text">The serialized value representation..</param>
    sealed class CssAnyValue(String text) : ICssRawValue
    {
        #region Fields

        private readonly String _text = text;

        #endregion

        #region ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets the contained value. This is the same as CssText.
        /// </summary>
        public String Value => _text;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => _text;

        #endregion

        #region Methods

        ICssValue? ICssValue.Compute(ICssComputeContext context)
        {
            var converter = context.Converter;

            if (converter is not null && converter is not AnyValueConverter)
            {
                var value = converter.Convert(_text);
                return value?.Compute(context);
            }

            return null;
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue? other) => other is CssAnyValue o && _text.Equals(o.CssText);

        #endregion
    }
}
