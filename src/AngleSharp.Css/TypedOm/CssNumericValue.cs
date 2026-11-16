namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The base class of numeric values, i.e., of units and math expressions.
    /// https://www.w3.org/TR/css-typed-om-1/#numeric-value
    /// </summary>
    [DomName("CSSNumericValue")]
    public abstract class CssNumericValue : CssStyleValue, IEquatable<CssNumericValue>
    {
        #region Fields

        private readonly CssNumericType _type;

        #endregion

        #region ctor

        internal CssNumericValue(CssNumericType type)
        {
            _type = type;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Converts a plain number to a numeric value with the unit "number".
        /// </summary>
        /// <param name="value">The number.</param>
        public static implicit operator CssNumericValue(Double value) => new CssUnitValue(value, CssUnits.Number);

        /// <summary>
        /// Adds two numeric values.
        /// </summary>
        public static CssNumericValue operator +(CssNumericValue a, CssNumericValue b) => a.Add(b);

        /// <summary>
        /// Subtracts two numeric values.
        /// </summary>
        public static CssNumericValue operator -(CssNumericValue a, CssNumericValue b) => a.Sub(b);

        /// <summary>
        /// Multiplies two numeric values.
        /// </summary>
        public static CssNumericValue operator *(CssNumericValue a, CssNumericValue b) => a.Mul(b);

        /// <summary>
        /// Divides two numeric values.
        /// </summary>
        public static CssNumericValue operator /(CssNumericValue a, CssNumericValue b) => a.Div(b);

        /// <summary>
        /// Negates a numeric value.
        /// </summary>
        public static CssNumericValue operator -(CssNumericValue a) => Negate(a);

        #endregion

        #region Methods

        /// <summary>
        /// Adds the given values to the current one.
        /// </summary>
        /// <param name="values">The values to add.</param>
        /// <returns>The sum, which is a unit value if all units are the same.</returns>
        /// <exception cref="ArgumentException">The types of the values cannot be added.</exception>
        [DomName("add")]
        public CssNumericValue Add(params CssNumericValue[] values)
        {
            var items = Flatten<CssMathSum>(values, sum => sum.Values);

            if (TryGetCommonUnit(items, out var unit))
            {
                return new CssUnitValue(items.Sum(m => ((CssUnitValue)m).Value), unit);
            }

            return new CssMathSum(items.ToArray());
        }

        /// <summary>
        /// Subtracts the given values from the current one.
        /// </summary>
        /// <param name="values">The values to subtract.</param>
        /// <returns>The difference, which is a unit value if all units are the same.</returns>
        /// <exception cref="ArgumentException">The types of the values cannot be added.</exception>
        [DomName("sub")]
        public CssNumericValue Sub(params CssNumericValue[] values) =>
            Add(Check(values).Select(Negate).ToArray());

        /// <summary>
        /// Multiplies the current value with the given ones.
        /// </summary>
        /// <param name="values">The values to multiply with.</param>
        /// <returns>The product, which is a unit value if at most one value is not a number.</returns>
        /// <exception cref="ArgumentException">The types of the values cannot be multiplied.</exception>
        [DomName("mul")]
        public CssNumericValue Mul(params CssNumericValue[] values)
        {
            var items = Flatten<CssMathProduct>(values, product => product.Values);

            if (items.All(m => m is CssUnitValue))
            {
                var units = items.Cast<CssUnitValue>().ToArray();
                var dimensions = units.Where(m => m.Unit != CssUnits.Number).ToArray();

                if (dimensions.Length <= 1)
                {
                    var value = units.Aggregate(1.0, (product, item) => product * item.Value);
                    return new CssUnitValue(value, dimensions.Length == 1 ? dimensions[0].Unit : CssUnits.Number);
                }
            }

            return new CssMathProduct(items.ToArray());
        }

        /// <summary>
        /// Divides the current value by the given ones.
        /// </summary>
        /// <param name="values">The values to divide by.</param>
        /// <returns>The quotient.</returns>
        /// <exception cref="ArgumentException">The types of the values cannot be multiplied.</exception>
        /// <exception cref="ArgumentOutOfRangeException">A value is the number 0.</exception>
        [DomName("div")]
        public CssNumericValue Div(params CssNumericValue[] values)
        {
            var inverted = Check(values).Select(value =>
            {
                if (value is CssUnitValue unit && unit.Unit == CssUnits.Number && unit.Value == 0.0)
                {
                    throw new ArgumentOutOfRangeException(nameof(values), "Cannot divide by zero.");
                }

                return Invert(value);
            });

            return Mul(inverted.ToArray());
        }

        /// <summary>
        /// Gets the minimum of the current and the given values.
        /// </summary>
        /// <param name="values">The other values.</param>
        /// <returns>The minimum, which is a unit value if all units are the same.</returns>
        /// <exception cref="ArgumentException">The types of the values cannot be compared.</exception>
        [DomName("min")]
        public CssNumericValue Min(params CssNumericValue[] values)
        {
            var items = new[] { this }.Concat(Check(values)).ToArray();

            if (TryGetCommonUnit(items, out var unit))
            {
                return new CssUnitValue(items.Min(m => ((CssUnitValue)m).Value), unit);
            }

            return new CssMathMin(items);
        }

        /// <summary>
        /// Gets the maximum of the current and the given values.
        /// </summary>
        /// <param name="values">The other values.</param>
        /// <returns>The maximum, which is a unit value if all units are the same.</returns>
        /// <exception cref="ArgumentException">The types of the values cannot be compared.</exception>
        [DomName("max")]
        public CssNumericValue Max(params CssNumericValue[] values)
        {
            var items = new[] { this }.Concat(Check(values)).ToArray();

            if (TryGetCommonUnit(items, out var unit))
            {
                return new CssUnitValue(items.Max(m => ((CssUnitValue)m).Value), unit);
            }

            return new CssMathMax(items);
        }

        /// <summary>
        /// Checks if the current value is structurally equal to all the given values.
        /// </summary>
        /// <param name="values">The values to compare to.</param>
        /// <returns>True if all are equal, otherwise false.</returns>
        [DomName("equals")]
        public Boolean Equals(params CssNumericValue[] values) => Check(values).All(IsEqualTo);

        /// <summary>
        /// Checks if the current value is structurally equal to the given value.
        /// </summary>
        /// <param name="other">The value to compare to.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssNumericValue? other) => other is not null && IsEqualTo(other);

        /// <summary>
        /// Checks if the current value is structurally equal to the given object.
        /// </summary>
        public override Boolean Equals(Object? obj) => obj is CssNumericValue other && IsEqualTo(other);

        /// <summary>
        /// Gets the hash code, which is derived from the serialization.
        /// </summary>
        public override Int32 GetHashCode() => StringComparer.Ordinal.GetHashCode(ToString());

        /// <summary>
        /// Converts the value to a single unit value of the given unit, e.g.,
        /// 1in to 96px or calc(1s + 500ms) to 1.5s.
        /// </summary>
        /// <param name="unit">The target unit, e.g., px.</param>
        /// <returns>The converted value.</returns>
        /// <exception cref="DomException">The unit is unknown.</exception>
        /// <exception cref="ArgumentException">The value cannot be converted to the unit.</exception>
        [DomName("to")]
        public CssUnitValue To(String unit)
        {
            var target = ValidateUnit(unit);
            var sum = ToSumValue();

            if (sum is not null && sum.Count == 1)
            {
                var item = sum[0];
                var source = item.GetSingleUnit();

                if (source is not null && CssUnits.TryConvert(item.Value, source, target, out var result))
                {
                    return new CssUnitValue(result, target);
                }
            }

            throw new ArgumentException($"The value '{this}' cannot be converted to '{target}'.", nameof(unit));
        }

        /// <summary>
        /// Converts the value to a sum of unit values, e.g., calc(1in + 1em + 1px)
        /// to calc(1em + 97px). If units are given, the sum contains exactly one
        /// value of each of the given units, in their order.
        /// </summary>
        /// <param name="units">The optional units to use.</param>
        /// <returns>The sum of unit values.</returns>
        /// <exception cref="DomException">A unit is unknown.</exception>
        /// <exception cref="ArgumentException">The value cannot be converted.</exception>
        [DomName("toSum")]
        public CssMathSum ToSum(params String[] units)
        {
            var targets = (units ?? Array.Empty<String>()).Select(ValidateUnit).ToArray();
            var sum = ToSumValue() ?? throw new ArgumentException($"The value '{this}' cannot be converted to a sum.");
            var items = new List<KeyValuePair<String, Double>>();

            foreach (var item in sum)
            {
                var unit = item.GetSingleUnit() ?? throw new ArgumentException($"The value '{this}' contains a product of units.");
                items.Add(new KeyValuePair<String, Double>(unit, item.Value));
            }

            if (targets.Length == 0)
            {
                var values = items
                    .OrderBy(m => m.Key, StringComparer.Ordinal)
                    .Select(m => (CssNumericValue)new CssUnitValue(m.Value, m.Key))
                    .ToArray();
                return new CssMathSum(values);
            }

            var result = new List<CssNumericValue>();

            foreach (var target in targets)
            {
                var total = 0.0;

                for (var i = items.Count - 1; i >= 0; i--)
                {
                    if (CssUnits.TryConvert(items[i].Value, items[i].Key, target, out var converted))
                    {
                        total += converted;
                        items.RemoveAt(i);
                    }
                }

                result.Add(new CssUnitValue(total, target));
            }

            if (items.Count > 0)
            {
                throw new ArgumentException($"The value '{this}' cannot be expressed in the given units.", nameof(units));
            }

            return new CssMathSum(result.ToArray());
        }

        /// <summary>
        /// Gets the type of the value, i.e., the exponents of its base types.
        /// </summary>
        /// <returns>The numeric type.</returns>
        [DomName("type")]
        public CssNumericType Type() => _type;

        /// <summary>
        /// Parses the given text as a numeric value, i.e., a number, a percentage, a
        /// dimension, or a math function such as calc().
        /// </summary>
        /// <param name="cssText">The text to parse, e.g., calc(1px + 2em).</param>
        /// <returns>The numeric value.</returns>
        /// <exception cref="DomException">The text is not a valid numeric value.</exception>
        [DomName("parse")]
        public static CssNumericValue Parse(String cssText)
        {
            if (cssText is null)
                throw new ArgumentNullException(nameof(cssText));

            var source = new StringSource(cssText);
            source.SkipSpacesAndComments();
            var value = source.ParseCalc() ?? source.ParseMath() ?? source.ParseAtomicExpression();
            source.SkipSpacesAndComments();

            if (value is not null && source.IsDone)
            {
                var result = CssStyleValueReifier.ReifyNumeric(value);

                if (result is not null)
                {
                    return result;
                }
            }

            throw new DomException(DomError.Syntax);
        }

        /// <summary>
        /// Serializes the value.
        /// https://www.w3.org/TR/css-typed-om-1/#serialize-a-cssnumericvalue
        /// </summary>
        public override String ToString()
        {
            var sb = new StringBuilder();
            Serialize(sb, false, false);
            return sb.ToString();
        }

        #endregion

        #region Internal Methods

        internal CssNumericType NumericType => _type;

        internal abstract void Serialize(StringBuilder sb, Boolean nested, Boolean parenless);

        internal abstract Boolean IsEqualTo(CssNumericValue other);

        /// <summary>
        /// Creates the sum value, or null in case of a failure, e.g., for min(1px, 1em).
        /// https://www.w3.org/TR/css-typed-om-1/#create-a-sum-value
        /// </summary>
        internal abstract List<CssSumValueItem>? ToSumValue();

        internal static CssNumericValue Negate(CssNumericValue value) => value switch
        {
            CssMathNegate negate => negate.Value,
            CssUnitValue unit => new CssUnitValue(-unit.Value, unit.Unit),
            _ => new CssMathNegate(value),
        };

        internal static CssNumericValue Invert(CssNumericValue value) => value switch
        {
            CssMathInvert invert => invert.Value,
            CssUnitValue unit when unit.Unit == CssUnits.Number => new CssUnitValue(1.0 / unit.Value, CssUnits.Number),
            _ => new CssMathInvert(value),
        };

        internal static CssNumericValue[] Check(CssNumericValue[] values)
        {
            if (values is null)
                throw new ArgumentNullException(nameof(values));

            foreach (var value in values)
            {
                if (value is null)
                    throw new ArgumentNullException(nameof(values));
            }

            return values;
        }

        internal static String ValidateUnit(String unit)
        {
            var normalized = CssUnits.Normalize(unit);

            if (!CssUnits.IsKnown(normalized))
            {
                throw new DomException(DomError.Syntax);
            }

            return normalized;
        }

        #endregion

        #region Helpers

        private List<CssNumericValue> Flatten<T>(CssNumericValue[] values, Func<T, IEnumerable<CssNumericValue>> getValues)
            where T : CssNumericValue
        {
            var items = new List<CssNumericValue>();

            foreach (var value in new[] { this }.Concat(Check(values)))
            {
                if (value is T composite)
                {
                    items.AddRange(getValues(composite));
                }
                else
                {
                    items.Add(value);
                }
            }

            return items;
        }

        private static Boolean TryGetCommonUnit(IEnumerable<CssNumericValue> values, out String unit)
        {
            unit = String.Empty;

            foreach (var value in values)
            {
                if (value is not CssUnitValue item || (unit.Length > 0 && item.Unit != unit))
                {
                    return false;
                }

                unit = item.Unit;
            }

            return unit.Length > 0;
        }

        #endregion
    }
}
