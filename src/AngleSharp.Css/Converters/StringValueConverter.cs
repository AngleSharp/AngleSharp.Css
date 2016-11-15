namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;

    sealed class StringValueConverter : IValueConverter
    {
        public ICssValue Convert(StringSource source)
        {
            var index = source.Index;
            var url = source.ParseString();
            return url != null ? new StringValue(source.Substring(index), url) : null;
        }

        private sealed class StringValue : BaseValue
        {
            private readonly String _content;

            public StringValue(String value, String content)
                : base(value)
            {
                _content = content;
            }
        }
    }
}
