namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    sealed class LocalFont : ICssFunctionValue
    {
        private readonly String _fontName;

        public LocalFont(String fontName)
        {
            _fontName = fontName;
        }

        public String Name => CssKeywords.Local;

        public ICssValue[] Arguments => new ICssValue[] { new Label(_fontName) };

        public String CssText => Name.CssFunction(Arguments.Join(", "));
    }
}
