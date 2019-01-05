namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
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
            var pos = source.Index;
            var ident = source.ParseIdent();
            var mode = default(T);

            if (ident != null && _values.TryGetValue(ident, out mode))
            {
                return new Constant<T>(ident.ToLowerInvariant(), mode);
            }

            source.BackTo(pos);
            return null;
        }
    }
}
