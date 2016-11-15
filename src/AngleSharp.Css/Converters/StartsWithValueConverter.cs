namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;

    sealed class StartsWithValueConverter : IValueConverter
    {
        private readonly String _start;
        private readonly IValueConverter _converter;

        public StartsWithValueConverter(String start, IValueConverter converter)
        {
            _start = start;
            _converter = converter;
        }

        public ICssValue Convert(StringSource source)
        {
            var rest = source.Content.Length - source.Index;

            if (rest >= _start.Length)
            {
                var length = 0;
                var current = source.Current;

                while (length < _start.Length)
                {
                    if (current != _start[length])
                        break;

                    length++;
                    current = source.Next();
                }

                if (length == _start.Length)
                {
                    source.SkipCurrentAndSpaces();
                    var data = _converter.Convert(source);
                    return data != null ? new StartValue(_start, data) : null;
                }
            }

            return null;
        }

        private sealed class StartValue : BaseValue
        {
            private readonly ICssValue _data;

            public StartValue(String value, ICssValue data)
                : base(value)
            {
                _data = data;
            }
        }
    }
}
