namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    sealed class DictionaryValueConverter<T> : IValueConverter
    {
        private readonly IDictionary<String, T> _values;

        public DictionaryValueConverter(IDictionary<String, T> values)
        {
            _values = values;
        }

        public ICssValue Convert(StringSource source)
        {
            var ident = source.ParseIdent();
            var mode = default(T);
            return ident != null && _values.TryGetValue(ident, out mode) ?
                new EnumeratedValue(ident, mode) : null;
        }

        private sealed class EnumeratedValue : BaseValue
        {
            private readonly T _data;

            public EnumeratedValue(String value, T data)
                : base(value)
            {
                _data = data;
            }
        }
    }
}
