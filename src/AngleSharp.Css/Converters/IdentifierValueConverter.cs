namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
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
            return result != null ? new IdentifierValue(result) : null;
        }

        private sealed class IdentifierValue : BaseValue
        {
            public IdentifierValue(String value)
                : base(value)
            {
            }
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
            return source.IsIdentifier(_identifier) ? new IdentifierValue(_identifier, _result) : null;
        }

        private sealed class IdentifierValue : BaseValue
        {
            private readonly T _data;

            public IdentifierValue(String value, T data)
                : base(value)
            {
                _data = data;
            }
        }
    }
}
