namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    sealed class DictionaryValueConverter<T>(IDictionary<String, T> values) : IValueConverter
    {
        private readonly IDictionary<String, T> _values = values;

        public ICssValue? Convert(StringSource source)
        {
            var pos = source.Index;
            var ident = source.ParseIdent();

            if (ident is not null && _values.TryGetValue(ident, out var mode))
            {
                return new CssConstantValue<T>(ident.ToLowerInvariant(), mode);
            }

            source.BackTo(pos);
            return null;
        }
    }
}
