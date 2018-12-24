namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;

    sealed class IdentifierValueConverter : IValueConverter
    {
        private readonly Func<StringSource, String> _check;

        public IdentifierValueConverter(Func<StringSource, String> check)
        {
            _check = check;
        }

        public ICssValue Convert(StringSource source)
        {
            var result = _check.Invoke(source);

            if (result != null)
            {
                return new Identifier(result);
            }

            return null;
        }
    }

    sealed class IdentifierValueConverter<T> : IValueConverter
    {
        private readonly String _identifier;
        private readonly T _result;

        public IdentifierValueConverter(String identifier, T result)
        {
            _identifier = identifier;
            _result = result;
        }

        public ICssValue Convert(StringSource source)
        {
            return source.IsIdentifier(_identifier) ? new Constant<T>(_identifier, _result) : null;
        }
    }
}
