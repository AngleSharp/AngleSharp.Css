namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;
    using System.IO;

    sealed class UrlValueConverter : IValueConverter
    {
        public ICssValue Convert(StringSource source)
        {
            var url = source.ParseUri();
            return url != null ? new UrlValue(url) : null;
        }

        private sealed class UrlValue : ICssValue
        {
            private readonly Url _url;

            public UrlValue(Url url)
            {
                _url = url;
            }

            public String CssText
            {
                get { return FunctionNames.Url.CssFunction(_url.Href.CssString()); }
            }

            public void ToCss(TextWriter writer, IStyleFormatter formatter)
            {
                writer.Write(CssText);
            }
        }
    }
}
