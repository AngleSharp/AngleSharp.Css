#nullable enable
namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a font-size keyword, e.g., medium or larger. An absolute keyword computes
    /// to a multiple of the device's default font size, a relative keyword to a multiple of
    /// the parent's font size.
    /// </summary>
    sealed class CssFontSizeKeywordValue : ICssPrimitiveValue, IEquatable<CssFontSizeKeywordValue>
    {
        #region Fields

        private readonly String _keyword;
        private readonly CssLengthValue _size;

        #endregion

        #region ctor

        public CssFontSizeKeywordValue(String keyword, CssLengthValue size)
        {
            _keyword = keyword;
            _size = size;
        }

        #endregion

        #region Properties

        public String CssText => _keyword;

        /// <summary>
        /// Gets the size relative to the default (em) or the parent's (%) font size.
        /// </summary>
        public CssLengthValue Size => _size;

        #endregion

        #region Methods

        public Boolean Equals(CssFontSizeKeywordValue? other) => other is not null && _keyword.Is(other._keyword);

        Boolean IEquatable<ICssValue>.Equals(ICssValue? other) => other is CssFontSizeKeywordValue value && Equals(value);

        public override Boolean Equals(Object? obj) => obj is CssFontSizeKeywordValue value && Equals(value);

        public override Int32 GetHashCode() => _keyword.GetHashCode();

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var basis = _size.Type == CssLengthValue.Unit.Percent ?
                (context as IRelativeComputeContext)?.PercentageBasis :
                context.Device?.FontSize;

            if (basis.HasValue && basis.Value > 0)
            {
                var factor = _size.Type == CssLengthValue.Unit.Percent ? _size.Value * 0.01 : _size.Value;
                ICssValue length = new CssLengthValue(basis.Value * factor, CssLengthValue.Unit.Px);
                return length.Compute(context);
            }

            return this;
        }

        #endregion
    }
}
