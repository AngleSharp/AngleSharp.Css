namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;
    using System.IO;

    sealed class StringValueConverter : IValueConverter
    {
        public ICssValue Convert(StringSource source)
        {
            var str = source.ParseString();
            return str != null ? new StringValue(str) : null;
        }

        private sealed class StringValue : ICssValue
        {
            private readonly String _content;

            public StringValue(String content)
            {
                _content = content;
            }

            public String CssText
            {
                get { return _content.CssString(); }
            }

            public void ToCss(TextWriter writer, IStyleFormatter formatter)
            {
                writer.Write(CssText);
            }
        }
    }
}
