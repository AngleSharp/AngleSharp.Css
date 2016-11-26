namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;
    using System.IO;

    sealed class ArgumentsValueConverter : IValueConverter
    {
        private readonly IValueConverter[] _converters;

        public ArgumentsValueConverter(params IValueConverter[] converters)
        {
            _converters = converters;
        }

        public ICssValue Convert(StringSource source)
        {
            var index = 0;
            var n = _converters.Length;
            var args = new ICssValue[n];

            while (index < n)
            {
                var arg = _converters[index].Convert(source);
                var current = source.SkipSpacesAndComments();
                args[index++] = arg;

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
            }

            source.SkipCurrentAndSpaces();
            return new ArgumentsValue(args);
        }

        private sealed class ArgumentsValue : ICssValue
        {
            private readonly ICssValue[] _arguments;

            public ArgumentsValue(ICssValue[] arguments)
            {
                _arguments = arguments;
            }

            public String CssText
            {
                get { return _arguments.Join(", "); }
            }

            public void ToCss(TextWriter writer, IStyleFormatter formatter)
            {
                writer.Write(CssText);
            }
        }
    }
}
