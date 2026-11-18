namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Represents a single numeric value with a unit, e.g., 10px, 50%, or 2 (with
    /// the unit "number").
    /// https://www.w3.org/TR/css-typed-om-1/#cssunitvalue
    /// </summary>
    [DomName("CSSUnitValue")]
    public sealed class CssUnitValue : CssNumericValue
    {
        #region Fields

        private readonly String _unit;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new unit value.
        /// </summary>
        /// <param name="value">The value, e.g., 10.</param>
        /// <param name="unit">The unit, e.g., px, percent, or number.</param>
        /// <exception cref="ArgumentException">The unit is unknown.</exception>
        [DomConstructor]
        public CssUnitValue(Double value, String unit)
            : base(CreateType(unit))
        {
            Value = value;
            _unit = CssUnits.Normalize(unit);
        }

        private static CssNumericType CreateType(String unit)
        {
            var normalized = CssUnits.Normalize(unit);

            if (!CssUnits.IsKnown(normalized))
            {
                throw new ArgumentException($"The unit '{unit}' is unknown.", nameof(unit));
            }

            return CssNumericType.ForUnit(normalized);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the numeric value.
        /// </summary>
        [DomName("value")]
        public Double Value { get; set; }

        /// <summary>
        /// Gets the unit, e.g., px, percent, or number.
        /// </summary>
        [DomName("unit")]
        public String Unit => _unit;

        #endregion

        #region Internal Methods

        internal override void Serialize(StringBuilder sb, Boolean nested, Boolean parenless) =>
            sb.Append(CssUnits.Serialize(Value, _unit));

        internal override Boolean IsEqualTo(CssNumericValue other) =>
            other is CssUnitValue unit && unit._unit == _unit && unit.Value.Equals(Value);

        internal override List<CssSumValueItem>? ToSumValue()
        {
            var value = Value;
            var unit = CssUnits.ToCanonical(_unit, ref value);
            return new List<CssSumValueItem> { CssSumValueItem.Create(value, unit) };
        }

        #endregion
    }
}
