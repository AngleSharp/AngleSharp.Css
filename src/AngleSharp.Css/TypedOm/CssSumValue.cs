namespace AngleSharp.Css.TypedOm
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A term of a sum value, i.e., a value with a map of units and their exponents.
    /// The units of absolute values are canonical (px, deg, ms, hz, dppx).
    /// https://www.w3.org/TR/css-typed-om-1/#create-a-sum-value
    /// </summary>
    sealed class CssSumValueItem
    {
        public CssSumValueItem(Double value, SortedDictionary<String, Int32> units)
        {
            Value = value;
            Units = units;
        }

        public Double Value { get; set; }

        public SortedDictionary<String, Int32> Units { get; }

        public static CssSumValueItem Create(Double value, String unit)
        {
            var units = new SortedDictionary<String, Int32>(StringComparer.Ordinal);

            if (unit != CssUnits.Number)
            {
                units[unit] = 1;
            }

            return new CssSumValueItem(value, units);
        }

        public CssSumValueItem Clone() => new(Value, new SortedDictionary<String, Int32>(Units, StringComparer.Ordinal));

        public Boolean HasSameUnits(CssSumValueItem other)
        {
            if (Units.Count != other.Units.Count)
            {
                return false;
            }

            foreach (var entry in Units)
            {
                if (!other.Units.TryGetValue(entry.Key, out var exponent) || exponent != entry.Value)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Gets the single unit with an exponent of 1 (or number), if the item has one.
        /// </summary>
        public String? GetSingleUnit()
        {
            if (Units.Count == 0)
            {
                return CssUnits.Number;
            }

            if (Units.Count == 1)
            {
                foreach (var entry in Units)
                {
                    if (entry.Value == 1)
                    {
                        return entry.Key;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Adds the item to the list, merging it into an item with the same units.
        /// </summary>
        public static void AddTo(List<CssSumValueItem> items, CssSumValueItem item)
        {
            foreach (var existing in items)
            {
                if (existing.HasSameUnits(item))
                {
                    existing.Value += item.Value;
                    return;
                }
            }

            items.Add(item);
        }

        /// <summary>
        /// Multiplies two items, i.e., multiplies the values and adds the exponents.
        /// </summary>
        public static CssSumValueItem Multiply(CssSumValueItem a, CssSumValueItem b)
        {
            var units = new SortedDictionary<String, Int32>(a.Units, StringComparer.Ordinal);

            foreach (var entry in b.Units)
            {
                units.TryGetValue(entry.Key, out var exponent);
                exponent += entry.Value;

                if (exponent == 0)
                {
                    units.Remove(entry.Key);
                }
                else
                {
                    units[entry.Key] = exponent;
                }
            }

            return new CssSumValueItem(a.Value * b.Value, units);
        }

        /// <summary>
        /// Gets the single items of the given sums, if they all have exactly one item
        /// and all items have the same units, as required for min(), max(), and clamp().
        /// </summary>
        public static List<CssSumValueItem>? GetComparableItems(IEnumerable<CssNumericValue> values)
        {
            var items = new List<CssSumValueItem>();

            foreach (var value in values)
            {
                var sum = value.ToSumValue();

                if (sum is null || sum.Count != 1 || (items.Count > 0 && !items[0].HasSameUnits(sum[0])))
                {
                    return null;
                }

                items.Add(sum[0]);
            }

            return items;
        }
    }
}
