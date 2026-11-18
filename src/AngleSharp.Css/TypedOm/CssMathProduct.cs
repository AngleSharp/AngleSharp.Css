namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Represents a product of numeric values, e.g., calc(2 * 1em). A division is
    /// the product with an inverted value.
    /// https://www.w3.org/TR/css-typed-om-1/#cssmathproduct
    /// </summary>
    [DomName("CSSMathProduct")]
    public sealed class CssMathProduct : CssMathValue
    {
        private readonly CssNumericArray _values;

        /// <summary>
        /// Creates a new product of the given values.
        /// </summary>
        /// <param name="values">The values to multiply, at least one.</param>
        /// <exception cref="AngleSharp.Dom.DomException">No value is given.</exception>
        /// <exception cref="ArgumentException">The types of the values cannot be multiplied.</exception>
        [DomConstructor]
        public CssMathProduct(params CssNumericValue[] values)
            : base(MultiplyTypes(values))
        {
            _values = new CssNumericArray(CheckArguments(values));
        }

        /// <summary>
        /// Gets the values to multiply.
        /// </summary>
        [DomName("values")]
        public CssNumericArray Values => _values;

        /// <summary>
        /// Gets the product operator.
        /// </summary>
        public override CssMathOperator Operator => CssMathOperator.Product;

        internal override void Serialize(StringBuilder sb, Boolean nested, Boolean parenless)
        {
            Open(sb, nested, parenless);
            _values[0].Serialize(sb, true, false);

            for (var i = 1; i < _values.Length; i++)
            {
                if (_values[i] is CssMathInvert invert)
                {
                    sb.Append(" / ");
                    invert.Value.Serialize(sb, true, false);
                }
                else
                {
                    sb.Append(" * ");
                    _values[i].Serialize(sb, true, false);
                }
            }

            Close(sb, parenless);
        }

        internal override Boolean IsEqualTo(CssNumericValue other) =>
            other is CssMathProduct product && AreEqual(_values, product._values);

        internal override List<CssSumValueItem>? ToSumValue()
        {
            var result = new List<CssSumValueItem> { CssSumValueItem.Create(1.0, CssUnits.Number) };

            foreach (var value in _values)
            {
                var sum = value.ToSumValue();

                if (sum is null)
                {
                    return null;
                }

                var temp = new List<CssSumValueItem>();

                foreach (var a in result)
                {
                    foreach (var b in sum)
                    {
                        CssSumValueItem.AddTo(temp, CssSumValueItem.Multiply(a, b));
                    }
                }

                result = temp;
            }

            return result;
        }
    }
}
