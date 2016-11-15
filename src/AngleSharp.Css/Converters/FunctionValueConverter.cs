namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;

    sealed class FunctionValueConverter : IValueConverter
    {
        private readonly String _name;
        private readonly IValueConverter _arguments;

        public FunctionValueConverter(String name, IValueConverter arguments)
        {
            _name = name;
            _arguments = arguments;
        }

        public ICssValue Convert(StringSource source)
        {
            var index = source.Index;
            var rest = source.Content.Length - index;

            if (rest >= _name.Length + 2)
            {
                var length = 0;
                var current = source.Current;

                while (length < _name.Length)
                {
                    if (Char.ToLowerInvariant(current) != _name[length])
                        break;

                    length++;
                    current = source.Next();
                }

                if (length == _name.Length && current == Symbols.RoundBracketOpen)
                {
                    source.SkipCurrentAndSpaces();
                    var args = _arguments.Convert(source);

                    if (args != null)
                    {
                        return new FunctionValue(source.Substring(index), _name, args);
                    }
                }
            }

            return null;
        }

        private sealed class FunctionValue : BaseValue
        {
            private readonly String _name;
            private readonly ICssValue _arguments;

            public FunctionValue(String value, String name, ICssValue arguments)
                : base(value)
            {
                _name = name;
                _arguments = arguments;
            }
        }
    }
}
