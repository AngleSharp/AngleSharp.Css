namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;

    sealed class UrlValueConverter : IValueConverter
    {
        public ICssValue Convert(StringSource source)
        {
            var index = source.Index;
            var url = source.ParseUri();
            return url != null ? new UrlValue(source.Substring(index), url) : null;
        }

        private sealed class UrlValue : BaseValue
        {
            private readonly Url _url;

            public UrlValue(String value, Url url)
                : base(value)
            {
                _url = url;
            }
        }
    }
}
