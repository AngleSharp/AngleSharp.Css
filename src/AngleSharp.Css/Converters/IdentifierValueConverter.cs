namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;

    sealed class IdentifierValueConverter(Func<StringSource, String> check) : IValueConverter
    {
        private readonly Func<StringSource, String> _check = check;

        public ICssValue? Convert(StringSource source)
        {
            var result = _check.Invoke(source);

            if (result is not null)
            {
                return new CssIdentifierValue(result);
            }

            return null;
        }
    }

    sealed class IdentifierValueConverter<T>(String identifier, T result) : IValueConverter
    {
        private readonly String _identifier = identifier;
        private readonly T _result = result;

        public ICssValue? Convert(StringSource source)
        {
            if (source.IsIdentifier(_identifier))
            {
                return new CssConstantValue<T>(_identifier, _result);
            }

            return null;
        }
    }
}
