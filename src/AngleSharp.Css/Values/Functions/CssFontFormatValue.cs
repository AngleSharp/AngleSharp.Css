namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    sealed class CssFontFormatValue : ICssFunctionValue
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

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            return this;
        }

        #endregion
    }
}
