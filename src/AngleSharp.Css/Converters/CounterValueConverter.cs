namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Extensions;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.IO;

    sealed class CounterValueConverter : IValueConverter
    {
        private readonly Int32 _defaultValue;

        public CounterValueConverter(Int32 defaultValue)
        {
            _defaultValue = defaultValue;
        }

        public ICssValue Convert(StringSource source)
        {
            var counters = new List<Counter>();

            while (!source.IsDone)
            {
                var name = source.ParseIdent();
                source.SkipSpacesAndComments();
                var value = source.ToInteger() ?? _defaultValue;
                source.SkipSpacesAndComments();

                if (name == null)
                {
                    return null;
                }

                counters.Add(new Counter
                {
                    Name = name,
                    Value = value
                });
            }
            
            return new CounterValue(counters.ToArray());
        }

        struct Counter
        {
            public String Name;
            public Int32 Value;

            public override String ToString()
            {
                return String.Concat(Name, " ", Value.ToString());
            }
        }

        private sealed class CounterValue : ICssValue
        {
            private readonly Counter[] _counters;

            public CounterValue(Counter[] counters)
            {
                _counters = counters;
            }

            public String CssText
            {
                get { return _counters.Join(" "); }
            }

            public void ToCss(TextWriter writer, IStyleFormatter formatter)
            {
                writer.Write(CssText);
            }
        }
    }
}
