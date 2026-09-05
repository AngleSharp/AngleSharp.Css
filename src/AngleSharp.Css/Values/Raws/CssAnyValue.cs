#nullable disable
namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents an unknown (any) value.
    /// </summary>
    sealed class CssAnyValue : ICssRawValue
    {
        #region Fields

        private readonly String _text;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new unknown value with the given literal content.
        /// </summary>
        /// <param name="text">The serialized value representation..</param>
        /// <param name="isResolved">Whether variable substitution has already been performed.</param>
        public CssAnyValue(String text, Boolean isResolved = false)
        {
            _text = text;
            IsResolved = isResolved;
        }

        internal Boolean IsResolved { get; }

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

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var converter = context.Converter;

            if (converter is not null && converter is not AnyValueConverter)
            {
                var source = new StringSource(_text);
                source.SkipSpacesAndComments();
                var value = converter.Convert(source);
                source.SkipSpacesAndComments();
                return source.IsDone ? value?.Compute(context) : null;
            }

            return this;
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssAnyValue o && _text == o.CssText;

        #endregion
    }
}
