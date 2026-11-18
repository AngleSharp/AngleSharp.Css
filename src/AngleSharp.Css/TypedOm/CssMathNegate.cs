namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Represents the negation of a numeric value, e.g., the subtracted value of
    /// calc(1px - 2em).
    /// https://www.w3.org/TR/css-typed-om-1/#cssmathnegate
    /// </summary>
    [DomName("CSSMathNegate")]
    public sealed class CssMathNegate : CssMathValue
    {
        private readonly CssNumericValue _value;

        /// <summary>
        /// Creates a new negation of the given value.
        /// </summary>
        /// <param name="value">The value to negate.</param>
        [DomConstructor]
        public CssMathNegate(CssNumericValue value)
            : base((value ?? throw new ArgumentNullException(nameof(value))).NumericType)
        {
            _value = value;
        }

        /// <summary>
        /// Gets the negated value.
        /// </summary>
        [DomName("value")]
        public CssNumericValue Value => _value;

        /// <summary>
        /// Gets the negate operator.
        /// </summary>
        public override CssMathOperator Operator => CssMathOperator.Negate;

        internal override void Serialize(StringBuilder sb, Boolean nested, Boolean parenless)
        {
            Open(sb, nested, parenless);
            sb.Append('-');
            _value.Serialize(sb, true, false);
            Close(sb, parenless);
        }

        internal override Boolean IsEqualTo(CssNumericValue other) =>
            other is CssMathNegate negate && _value.IsEqualTo(negate._value);

        internal override List<CssSumValueItem>? ToSumValue()
        {
            var sum = _value.ToSumValue();

            if (sum is not null)
            {
                foreach (var item in sum)
                {
                    item.Value = -item.Value;
                }
            }

            return sum;
        }
    }
}
