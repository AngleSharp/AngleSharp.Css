namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    sealed class FontFormat : ICssFunctionValue
    {
        private readonly String _fontFormat;

        public FontFormat(String fontFormat)
        {
            _fontFormat = fontFormat;
        }

        public String Name => CssKeywords.Format;

        public ICssValue[] Arguments => new[] { new StringValue(_fontFormat) };

        public String CssText => Name.CssFunction(Arguments.Join(", "));
    }
}
