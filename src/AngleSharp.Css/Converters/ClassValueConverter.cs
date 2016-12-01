namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.IO;

    sealed class ClassValueConverter<T> : IValueConverter
        where T : class
    {
        private readonly Func<StringSource, T> _converter;

        public ClassValueConverter(Func<StringSource, T> converter)
        {
            _converter = converter;
        }

        public ICssValue Convert(StringSource source)
        {
            var result = _converter.Invoke(source);
            return result != null ? new ClassValue(result) : null;
        }

        private sealed class ClassValue : ICssValue
        {
            private readonly T _data;

            public ClassValue(T data)
            {
                _data = data;
            }

            public String CssText
            {
                get { return _data.ToString(); }
            }

            public void ToCss(TextWriter writer, IStyleFormatter formatter)
            {
                writer.Write(CssText);
            }
        }
    }
}
