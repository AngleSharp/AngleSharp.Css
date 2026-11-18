namespace AngleSharp.Css.TypedOm
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The units known to the Typed OM, with their base type and, where the
    /// unit is absolute, the factor converting it to the canonical unit.
    /// https://www.w3.org/TR/css-typed-om-1/#numeric-typing
    /// </summary>
    static class CssUnits
    {
        public const String Number = "number";
        public const String Percent = "percent";

        private static readonly Dictionary<String, UnitInfo> Units = new(StringComparer.Ordinal)
        {
            { Number, new UnitInfo(null, null, 1.0) },
            { Percent, new UnitInfo(CssNumericBaseType.Percent, null, 1.0) },

            // absolute lengths
            { "px", new UnitInfo(CssNumericBaseType.Length, "px", 1.0) },
            { "cm", new UnitInfo(CssNumericBaseType.Length, "px", 96.0 / 2.54) },
            { "mm", new UnitInfo(CssNumericBaseType.Length, "px", 96.0 / 25.4) },
            { "q", new UnitInfo(CssNumericBaseType.Length, "px", 96.0 / 101.6) },
            { "in", new UnitInfo(CssNumericBaseType.Length, "px", 96.0) },
            { "pt", new UnitInfo(CssNumericBaseType.Length, "px", 96.0 / 72.0) },
            { "pc", new UnitInfo(CssNumericBaseType.Length, "px", 16.0) },

            // relative lengths
            { "em", Relative(CssNumericBaseType.Length) },
            { "rem", Relative(CssNumericBaseType.Length) },
            { "ex", Relative(CssNumericBaseType.Length) },
            { "rex", Relative(CssNumericBaseType.Length) },
            { "ch", Relative(CssNumericBaseType.Length) },
            { "rch", Relative(CssNumericBaseType.Length) },
            { "ic", Relative(CssNumericBaseType.Length) },
            { "ric", Relative(CssNumericBaseType.Length) },
            { "cap", Relative(CssNumericBaseType.Length) },
            { "rcap", Relative(CssNumericBaseType.Length) },
            { "lh", Relative(CssNumericBaseType.Length) },
            { "rlh", Relative(CssNumericBaseType.Length) },
            { "vw", Relative(CssNumericBaseType.Length) },
            { "vh", Relative(CssNumericBaseType.Length) },
            { "vi", Relative(CssNumericBaseType.Length) },
            { "vb", Relative(CssNumericBaseType.Length) },
            { "vmin", Relative(CssNumericBaseType.Length) },
            { "vmax", Relative(CssNumericBaseType.Length) },
            { "svw", Relative(CssNumericBaseType.Length) },
            { "svh", Relative(CssNumericBaseType.Length) },
            { "svi", Relative(CssNumericBaseType.Length) },
            { "svb", Relative(CssNumericBaseType.Length) },
            { "svmin", Relative(CssNumericBaseType.Length) },
            { "svmax", Relative(CssNumericBaseType.Length) },
            { "lvw", Relative(CssNumericBaseType.Length) },
            { "lvh", Relative(CssNumericBaseType.Length) },
            { "lvi", Relative(CssNumericBaseType.Length) },
            { "lvb", Relative(CssNumericBaseType.Length) },
            { "lvmin", Relative(CssNumericBaseType.Length) },
            { "lvmax", Relative(CssNumericBaseType.Length) },
            { "dvw", Relative(CssNumericBaseType.Length) },
            { "dvh", Relative(CssNumericBaseType.Length) },
            { "dvi", Relative(CssNumericBaseType.Length) },
            { "dvb", Relative(CssNumericBaseType.Length) },
            { "dvmin", Relative(CssNumericBaseType.Length) },
            { "dvmax", Relative(CssNumericBaseType.Length) },
            { "cqw", Relative(CssNumericBaseType.Length) },
            { "cqh", Relative(CssNumericBaseType.Length) },
            { "cqi", Relative(CssNumericBaseType.Length) },
            { "cqb", Relative(CssNumericBaseType.Length) },
            { "cqmin", Relative(CssNumericBaseType.Length) },
            { "cqmax", Relative(CssNumericBaseType.Length) },

            // angles
            { "deg", new UnitInfo(CssNumericBaseType.Angle, "deg", 1.0) },
            { "rad", new UnitInfo(CssNumericBaseType.Angle, "deg", 180.0 / Math.PI) },
            { "grad", new UnitInfo(CssNumericBaseType.Angle, "deg", 0.9) },
            { "turn", new UnitInfo(CssNumericBaseType.Angle, "deg", 360.0) },

            // times
            { "ms", new UnitInfo(CssNumericBaseType.Time, "ms", 1.0) },
            { "s", new UnitInfo(CssNumericBaseType.Time, "ms", 1000.0) },

            // frequencies
            { "hz", new UnitInfo(CssNumericBaseType.Frequency, "hz", 1.0) },
            { "khz", new UnitInfo(CssNumericBaseType.Frequency, "hz", 1000.0) },

            // resolutions
            { "dppx", new UnitInfo(CssNumericBaseType.Resolution, "dppx", 1.0) },
            { "x", new UnitInfo(CssNumericBaseType.Resolution, "dppx", 1.0) },
            { "dpi", new UnitInfo(CssNumericBaseType.Resolution, "dppx", 1.0 / 96.0) },
            { "dpcm", new UnitInfo(CssNumericBaseType.Resolution, "dppx", 2.54 / 96.0) },

            // flexible lengths
            { "fr", Relative(CssNumericBaseType.Flex) },
        };

        private static UnitInfo Relative(CssNumericBaseType type) => new(type, null, 1.0);

        /// <summary>
        /// Normalizes the given unit, i.e., lower cases it and maps "%" to "percent".
        /// </summary>
        public static String Normalize(String unit)
        {
            if (unit is null)
            {
                return String.Empty;
            }

            var lower = unit.ToLowerInvariant();
            return lower == "%" ? Percent : lower;
        }

        /// <summary>
        /// Checks if the (normalized) unit is known.
        /// </summary>
        public static Boolean IsKnown(String unit) => Units.ContainsKey(unit);

        /// <summary>
        /// Gets the base type of the (normalized and known) unit, if any.
        /// </summary>
        public static CssNumericBaseType? GetBaseType(String unit) => Units[unit].BaseType;

        /// <summary>
        /// Gets the canonical unit (and the converted value) for the given unit, which is the
        /// unit itself for units that cannot be converted.
        /// </summary>
        public static String ToCanonical(String unit, ref Double value)
        {
            var info = Units[unit];

            if (info.Canonical is not null)
            {
                value *= info.Factor;
                return info.Canonical;
            }

            return unit;
        }

        /// <summary>
        /// Tries to convert the value from one unit to another one.
        /// </summary>
        public static Boolean TryConvert(Double value, String from, String to, out Double result)
        {
            result = value;

            if (from == to)
            {
                return true;
            }

            if (Units.TryGetValue(from, out var source) && Units.TryGetValue(to, out var target) &&
                source.Canonical is not null && source.Canonical == target.Canonical)
            {
                result = value * source.Factor / target.Factor;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the serialization of a value in the given unit.
        /// </summary>
        public static String Serialize(Double value, String unit)
        {
            var text = value.CssStringify();

            return unit switch
            {
                Number => text,
                Percent => text + "%",
                _ => text + unit,
            };
        }

        private readonly struct UnitInfo
        {
            public UnitInfo(CssNumericBaseType? baseType, String? canonical, Double factor)
            {
                BaseType = baseType;
                Canonical = canonical;
                Factor = factor;
            }

            public CssNumericBaseType? BaseType { get; }

            public String? Canonical { get; }

            public Double Factor { get; }
        }
    }
}
