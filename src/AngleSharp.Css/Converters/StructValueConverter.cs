namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;

    sealed class StructValueConverter<T> : IValueConverter
        where T : struct, IFormattable
    {
        private readonly Func<StringSource, T?> _converter;

        public StructValueConverter(Func<StringSource, T?> converter)
        {
            _converter = converter;
        }

        public ICssValue Convert(StringSource source)
        {
            var index = source.Index;
            var result = _converter.Invoke(source);
            return result.HasValue ? new StructValue(source.Substring(index), result.Value) : null;
        }

        private sealed class StructValue : BaseValue
        {
            private readonly T _data;

            public StructValue(String value, T data)
                : base(value)
            {
                _data = data;
            }
        }
    }
}
