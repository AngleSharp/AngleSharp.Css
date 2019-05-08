namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    sealed class CssFontFormatValue : ICssFunctionValue
    {
        private readonly String _fontFormat;

        public CssFontFormatValue(String fontFormat)
        {
            _fontFormat = fontFormat;
        }

        public String Name => CssKeywords.Format;

        public ICssValue[] Arguments => new ICssValue[] { new Label(_fontFormat) };

        public String CssText => Name.CssFunction(Arguments.Join(", "));
    }
}
