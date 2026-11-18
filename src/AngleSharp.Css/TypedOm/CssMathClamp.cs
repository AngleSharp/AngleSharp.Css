namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Represents a numeric value clamped between a lower and an upper bound, i.e., clamp().
    /// https://drafts.css-houdini.org/css-typed-om-1/#cssmathclamp
    /// </summary>
    [DomName("CSSMathClamp")]
    public sealed class CssMathClamp : CssMathValue
    {
        private readonly CssNumericValue _lower;
        private readonly CssNumericValue _value;
        private readonly CssNumericValue _upper;

        /// <summary>
        /// Creates a new clamped value.
        /// </summary>
        /// <param name="lower">The lower bound.</param>
        /// <param name="value">The preferred value.</param>
        /// <param name="upper">The upper bound.</param>
        /// <exception cref="ArgumentException">The types of the values cannot be compared.</exception>
        [DomConstructor]
        public CssMathClamp(CssNumericValue lower, CssNumericValue value, CssNumericValue upper)
            : base(AddTypes(lower, value, upper))
        {
            _lower = lower;
            _value = value;
            _upper = upper;
        }

        /// <summary>
        /// Gets the lower bound.
        /// </summary>
        [DomName("lower")]
        public CssNumericValue Lower => _lower;

        /// <summary>
        /// Gets the preferred value.
        /// </summary>
        [DomName("value")]
        public CssNumericValue Value => _value;

        /// <summary>
        /// Gets the upper bound.
        /// </summary>
        [DomName("upper")]
        public CssNumericValue Upper => _upper;

        /// <summary>
        /// Gets the clamp operator.
        /// </summary>
        public override CssMathOperator Operator => CssMathOperator.Clamp;

        internal override void Serialize(StringBuilder sb, Boolean nested, Boolean parenless) =>
            SerializeFunction(sb, "clamp", new[] { _lower, _value, _upper });

        internal override Boolean IsEqualTo(CssNumericValue other) =>
            other is CssMathClamp clamp && _lower.IsEqualTo(clamp._lower) && _value.IsEqualTo(clamp._value) && _upper.IsEqualTo(clamp._upper);

        internal override List<CssSumValueItem>? ToSumValue()
        {
            var items = CssSumValueItem.GetComparableItems(new[] { _lower, _value, _upper });

            if (items is null)
            {
                return null;
            }

            var result = items[1];
            result.Value = Math.Max(items[0].Value, Math.Min(items[1].Value, items[2].Value));
            return new List<CssSumValueItem> { result };
        }
    }
}
