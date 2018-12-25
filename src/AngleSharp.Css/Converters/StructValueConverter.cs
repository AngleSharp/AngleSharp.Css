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
            return _converter.Invoke(source);
        }
    }
}
