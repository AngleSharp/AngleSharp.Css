namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    sealed class CssLocalFontValue : ICssFunctionValue, IEquatable<CssLocalFontValue>
    {
        #region Fields

        private readonly String _fontName;

        #endregion

        #region ctor

        public CssLocalFontValue(String fontName)
        {
            _fontName = fontName;
        }

        #endregion

        #region Properties

        public String Name => CssKeywords.Local;

        public ICssValue[] Arguments => new ICssValue[] { new CssStringValue(_fontName) };

        public String CssText => Name.CssFunction(Arguments.Join(", "));

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssLocalFontValue other) => other is not null && _fontName == other._fontName;

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssLocalFontValue value && Equals(value);

        ICssValue ICssValue.Compute(ICssComputeContext context) => this;

        #endregion
    }
}
