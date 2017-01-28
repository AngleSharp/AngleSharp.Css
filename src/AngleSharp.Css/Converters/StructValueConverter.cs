namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    sealed class StructValueConverter<T> : IValueConverter
        where T : struct, ICssValue
    {
        private readonly Func<StringSource, T?> _converter;

        public StructValueConverter(Func<StringSource, T?> converter)
        {
            _converter = converter;
        }

        public ICssValue Convert(StringSource source)
        {
            var result = _converter.Invoke(source);
            return result.HasValue ? new StructValue(result.Value) : null;
        }

        private sealed class StructValue : ICssValue
        {
            private readonly T _data;

            public StructValue(T data)
            {
                _data = data;
            }

            public String CssText
            {
                get { return _data.CssText; }
            }
        }
    }
}
