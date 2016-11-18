namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;
    using System.IO;

    sealed class StartsWithValueConverter : IValueConverter
    {
        private readonly String _start;
        private readonly IValueConverter _converter;

        public StartsWithValueConverter(String start, IValueConverter converter)
        {
            _start = start;
            _converter = converter;
        }

        public ICssValue Convert(StringSource source)
        {
            var rest = source.Content.Length - source.Index;

            if (rest >= _start.Length)
            {
                var length = 0;
                var current = source.Current;

                while (length < _start.Length)
                {
                    if (current != _start[length])
                        break;

                    length++;
                    current = source.Next();
                }

                if (length == _start.Length)
                {
                    source.SkipSpacesAndComments();
                    var data = _converter.Convert(source);
                    return data != null ? new StartValue(_start, data) : null;
                }
            }

            return null;
        }

        private sealed class StartValue : ICssValue
        {
            private readonly String _start;
            private readonly ICssValue _data;

            public StartValue(String start, ICssValue data)
            {
                _start = start;
                _data = data;
            }

            public String CssText
            {
                get { return String.Concat(_start, " ", _data.CssText); }
            }

            public void ToCss(TextWriter writer, IStyleFormatter formatter)
            {
                writer.Write(CssText);
            }
        }
    }
}
