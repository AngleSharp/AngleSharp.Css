namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Represents the maximum of numeric values, i.e., max().
    /// https://www.w3.org/TR/css-typed-om-1/#cssmathmax
    /// </summary>
    [DomName("CSSMathMax")]
    public sealed class CssMathMax : CssMathValue
    {
        private readonly CssNumericArray _values;

        /// <summary>
        /// Creates a new maximum of the given values.
        /// </summary>
        /// <param name="values">The values to compare, at least one.</param>
        /// <exception cref="AngleSharp.Dom.DomException">No value is given.</exception>
        /// <exception cref="ArgumentException">The types of the values cannot be compared.</exception>
        [DomConstructor]
        public CssMathMax(params CssNumericValue[] values)
            : base(AddTypes(values))
        {
            _values = new CssNumericArray(CheckArguments(values));
        }

        /// <summary>
        /// Gets the values to compare.
        /// </summary>
        [DomName("values")]
        public CssNumericArray Values => _values;

        /// <summary>
        /// Gets the max operator.
        /// </summary>
        public override CssMathOperator Operator => CssMathOperator.Max;

        internal override void Serialize(StringBuilder sb, Boolean nested, Boolean parenless) =>
            SerializeFunction(sb, "max", _values);

        internal override Boolean IsEqualTo(CssNumericValue other) =>
            other is CssMathMax value && AreEqual(_values, value._values);

        internal override List<CssSumValueItem>? ToSumValue()
        {
            var items = CssSumValueItem.GetComparableItems(_values);
            return items is null ? null : new List<CssSumValueItem> { items.OrderBy(m => m.Value).Last() };
        }
    }
}
