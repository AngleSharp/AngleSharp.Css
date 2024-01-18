namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    sealed class CssLocalFontValue : ICssFunctionValue
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

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            return this;
        }

        #endregion
    }
}
