namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;

    sealed class PeriodicValueConverter : IValueConverter
    {
        private readonly IValueConverter _converter;

        public PeriodicValueConverter(IValueConverter converter)
        {
            _converter = converter;
        }

        public ICssValue Convert(StringSource source)
        {
            var start = source.Index;
            var options = new ICssValue[4];

            for (var i = 0; i < options.Length; i++)
            {
                options[i] = _converter.Convert(source);
                source.SkipSpaces();
            }

            return options[0] != null ? new PeriodicValue(source.Substring(start), options) : null;
        }

        private sealed class PeriodicValue : BaseValue
        {
            private readonly ICssValue _top;
            private readonly ICssValue _right; 
            private readonly ICssValue _bottom;
            private readonly ICssValue _left;

            public PeriodicValue(String value, ICssValue[] options)
                : base(value)
            {
                _top = options[0];
                _right = options[1] ?? _top;
                _bottom = options[2] ?? _top;
                _left = options[3] ?? _right;
            }
        }
    }
}
