namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Represents a sum of numeric values, e.g., calc(1px + 2em). A subtraction is
    /// the sum with a negated value.
    /// https://www.w3.org/TR/css-typed-om-1/#cssmathsum
    /// </summary>
    [DomName("CSSMathSum")]
    public sealed class CssMathSum : CssMathValue
    {
        private readonly CssNumericArray _values;

        /// <summary>
        /// Creates a new sum of the given values.
        /// </summary>
        /// <param name="values">The values to add, at least one.</param>
        /// <exception cref="AngleSharp.Dom.DomException">No value is given.</exception>
        /// <exception cref="ArgumentException">The types of the values cannot be added.</exception>
        [DomConstructor]
        public CssMathSum(params CssNumericValue[] values)
            : base(AddTypes(values))
        {
            _values = new CssNumericArray(CheckArguments(values));
        }

        /// <summary>
        /// Gets the values to add.
        /// </summary>
        [DomName("values")]
        public CssNumericArray Values => _values;

        /// <summary>
        /// Gets the sum operator.
        /// </summary>
        public override CssMathOperator Operator => CssMathOperator.Sum;

        internal override void Serialize(StringBuilder sb, Boolean nested, Boolean parenless)
        {
            Open(sb, nested, parenless);
            _values[0].Serialize(sb, true, false);

            for (var i = 1; i < _values.Length; i++)
            {
                if (_values[i] is CssMathNegate negate)
                {
                    sb.Append(" - ");
                    negate.Value.Serialize(sb, true, false);
                }
                else
                {
                    sb.Append(" + ");
                    _values[i].Serialize(sb, true, false);
                }
            }

            Close(sb, parenless);
        }

        internal override Boolean IsEqualTo(CssNumericValue other) =>
            other is CssMathSum sum && AreEqual(_values, sum._values);

        internal override List<CssSumValueItem>? ToSumValue()
        {
            var result = new List<CssSumValueItem>();

            foreach (var value in _values)
            {
                var sum = value.ToSumValue();

                if (sum is null)
                {
                    return null;
                }

                foreach (var item in sum)
                {
                    CssSumValueItem.AddTo(result, item);
                }
            }

            return result;
        }
    }
}
