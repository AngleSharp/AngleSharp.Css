namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Represents the minimum of numeric values, i.e., min().
    /// https://www.w3.org/TR/css-typed-om-1/#cssmathmin
    /// </summary>
    [DomName("CSSMathMin")]
    public sealed class CssMathMin : CssMathValue
    {
        private readonly CssNumericArray _values;

        /// <summary>
        /// Creates a new minimum of the given values.
        /// </summary>
        /// <param name="values">The values to compare, at least one.</param>
        /// <exception cref="AngleSharp.Dom.DomException">No value is given.</exception>
        /// <exception cref="ArgumentException">The types of the values cannot be compared.</exception>
        [DomConstructor]
        public CssMathMin(params CssNumericValue[] values)
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
        /// Gets the min operator.
        /// </summary>
        public override CssMathOperator Operator => CssMathOperator.Min;

        internal override void Serialize(StringBuilder sb, Boolean nested, Boolean parenless) =>
            SerializeFunction(sb, "min", _values);

        internal override Boolean IsEqualTo(CssNumericValue other) =>
            other is CssMathMin value && AreEqual(_values, value._values);

        internal override List<CssSumValueItem>? ToSumValue()
        {
            var items = CssSumValueItem.GetComparableItems(_values);
            return items is null ? null : new List<CssSumValueItem> { items.OrderBy(m => m.Value).First() };
        }
    }
}
