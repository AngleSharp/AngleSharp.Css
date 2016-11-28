namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;
    using System.IO;

    sealed class PeriodicValueConverter : IValueConverter
    {
        private readonly IValueConverter _converter;

        public PeriodicValueConverter(IValueConverter converter)
        {
            _converter = converter;
        }

        public ICssValue Convert(StringSource source)
        {
            var options = new ICssValue[4];

            for (var i = 0; i < options.Length; i++)
            {
                options[i] = _converter.Convert(source);
                source.SkipSpacesAndComments();
            }

            return options[0] != null ? new PeriodicValue(options) : null;
        }

        private sealed class PeriodicValue : ICssValue
        {
            private readonly ICssValue[] _values;

            public PeriodicValue(ICssValue[] values)
            {
                _values = values;
            }

            public String CssText
            {
                get
                {
                    var l = _values[3] != null ? 4 : _values[2] != null ? 3 : _values[1] != null ? 2 : 1;
                    var parts = new String[l];

                    for (var i = 0; i < l; i++)
                    {
                        parts[i] = _values[i].CssText;
                    }

                    if (l == 2 && parts[1] == parts[0])
                    {
                        parts = new[] { parts[0] };
                    }
                    
                    return String.Join(" ", parts);
                }
            }

            public ICssValue Top
            {
                get { return _values[0]; }
            }

            public ICssValue Right
            {
                get { return _values[1] ?? Top; }
            }

            public ICssValue Bottom
            {
                get { return _values[2] ?? Top; }
            }

            public ICssValue Left
            {
                get { return _values[3] ?? Right; }
            }

            public void ToCss(TextWriter writer, IStyleFormatter formatter)
            {
                writer.Write(CssText);
            }
        }
    }
}
