namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    sealed class StructValueConverter<T>(Func<StringSource, T?> converter) : IValueConverter
        where T : struct, ICssValue
    {
        private readonly Func<StringSource, T?> _converter = converter;

        public ICssValue? Convert(StringSource source) => _converter.Invoke(source);
    }
}
