#nullable enable
namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Converts an &lt;alpha-value&gt;, i.e., a number or a percentage. A percentage keeps its
    /// specified form, but computes to the equivalent number (e.g., 50% to 0.5).
    /// </summary>
    sealed class AlphaValueConverter : IValueConverter
    {
        private readonly IValueConverter _number;

        public AlphaValueConverter(IValueConverter number)
        {
            _number = number;
        }

        public ICssValue? Convert(StringSource source)
        {
            var pos = source.Index;
            var percent = source.ParsePercent();

            if (percent.HasValue)
            {
                return new AlphaPercentValue(percent.Value);
            }

            source.BackTo(pos);
            return _number.Convert(source);
        }

        sealed class AlphaPercentValue : ICssValue
        {
            private readonly Double _value;

            public AlphaPercentValue(Double value)
            {
                _value = value;
            }

            public String CssText => new CssPercentageValue(_value * 100.0).CssText;

            public ICssValue Compute(ICssComputeContext context) => new CssNumberValue(_value);

            public Boolean Equals(ICssValue? other) => other is AlphaPercentValue value && value._value == _value;
        }
    }
}
