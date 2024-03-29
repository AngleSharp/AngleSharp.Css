namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    sealed class CssFontFormatValue : ICssFunctionValue, IEquatable<CssFontFormatValue>
    {
        #region Fields

        private readonly String _fontFormat;

        #endregion

        #region ctor

        public CssFontFormatValue(String fontFormat)
        {
            _fontFormat = fontFormat;
        }

        #endregion

        #region Properties

        public String Name => CssKeywords.Format;

        public ICssValue[] Arguments => new ICssValue[] { new CssStringValue(_fontFormat) };

        public String CssText => Name.CssFunction(Arguments.Join(", "));

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssFontFormatValue other) => other is not null && _fontFormat == other._fontFormat;

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssFontFormatValue value && Equals(value);

        ICssValue ICssValue.Compute(ICssComputeContext context) => this;

        #endregion
    }
}
