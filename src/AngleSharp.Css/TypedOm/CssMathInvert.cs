namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Represents the reciprocal of a numeric value, e.g., the divisor of
    /// calc(1px / 2).
    /// https://www.w3.org/TR/css-typed-om-1/#cssmathinvert
    /// </summary>
    [DomName("CSSMathInvert")]
    public sealed class CssMathInvert : CssMathValue
    {
        private readonly CssNumericValue _value;

        /// <summary>
        /// Creates a new reciprocal of the given value.
        /// </summary>
        /// <param name="value">The value to invert.</param>
        [DomConstructor]
        public CssMathInvert(CssNumericValue value)
            : base((value ?? throw new ArgumentNullException(nameof(value))).NumericType.Invert())
        {
            _value = value;
        }

        /// <summary>
        /// Gets the inverted value.
        /// </summary>
        [DomName("value")]
        public CssNumericValue Value => _value;

        /// <summary>
        /// Gets the invert operator.
        /// </summary>
        public override CssMathOperator Operator => CssMathOperator.Invert;

        internal override void Serialize(StringBuilder sb, Boolean nested, Boolean parenless)
        {
            Open(sb, nested, parenless);
            sb.Append("1 / ");
            _value.Serialize(sb, true, false);
            Close(sb, parenless);
        }

        internal override Boolean IsEqualTo(CssNumericValue other) =>
            other is CssMathInvert invert && _value.IsEqualTo(invert._value);

        internal override List<CssSumValueItem>? ToSumValue()
        {
            var sum = _value.ToSumValue();

            if (sum is null || sum.Count != 1)
            {
                return null;
            }

            var item = sum[0];
            var units = new SortedDictionary<String, Int32>(StringComparer.Ordinal);

            foreach (var entry in item.Units)
            {
                units[entry.Key] = -entry.Value;
            }

            return new List<CssSumValueItem> { new(1.0 / item.Value, units) };
        }
    }
}
