namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    sealed class ClassValueConverter<T>(Func<StringSource, T> converter) : IValueConverter
        where T : class, ICssValue
    {
        private readonly Func<StringSource, T> _converter = converter;

        public ICssValue Convert(StringSource source)
        {
            return _converter.Invoke(source);
        }
    }
}
