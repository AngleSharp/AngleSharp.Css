namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;

    sealed class ArgumentsValueConverter : IValueConverter
    {
        private readonly IValueConverter[] _converters;

        public ArgumentsValueConverter(params IValueConverter[] converters)
        {
            _converters = converters;
        }

        public ICssValue Convert(StringSource source)
        {
            var start = source.Index;
            var index = 0;
            var n = _converters.Length;
            var args = new ICssValue[n];

            while (index < n)
            {
                var arg = _converters[index].Convert(source);
                var current = source.SkipSpaces();
                index++;

                if (arg == null)
                {
                    return null;
                }
                else if (index < n && current == Symbols.Comma)
                {
                    source.SkipCurrentAndSpaces();
                }
                else if (index != n || current != Symbols.RoundBracketClose)
                {
                    return null;
                }

                args[index] = arg;
            }

            var value = source.Substring(start);
            return new ArgumentsValue(value, args);
        }

        private sealed class ArgumentsValue : BaseValue
        {
            private readonly ICssValue[] _arguments;

            public ArgumentsValue(String value, ICssValue[] arguments)
                : base(value)
            {
                _arguments = arguments;
            }
        }
    }
}
