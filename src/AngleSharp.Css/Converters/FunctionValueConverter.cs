namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;
    using System.IO;

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
            if (source.IsFunction(_name))
            {
                var args = _arguments.Convert(source);

                if (args != null)
                {
                    return new FunctionValue(_name, args);
                }
            }

            return null;
        }

        private sealed class FunctionValue : ICssValue
        {
            private readonly String _name;
            private readonly ICssValue _arguments;

            public FunctionValue(String name, ICssValue arguments)
            {
                _name = name;
                _arguments = arguments;
            }

            public String CssText
            {
                get { return _name.CssFunction(_arguments.CssText); }
            }

            public void ToCss(TextWriter writer, IStyleFormatter formatter)
            {
                writer.Write(CssText);
            }
        }
    }
}
