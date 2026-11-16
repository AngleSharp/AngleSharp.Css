namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the type of a numeric value, i.e., the exponents of its base types.
    /// https://www.w3.org/TR/css-typed-om-1/#dictdef-cssnumerictype
    /// </summary>
    [DomName("CSSNumericType")]
    public sealed class CssNumericType : IEquatable<CssNumericType>
    {
        #region Fields

        private const Int32 BaseTypeCount = 7;

        private readonly Int32[] _exponents;
        private CssNumericBaseType? _percentHint;

        #endregion

        #region ctor

        private CssNumericType(Int32[] exponents, CssNumericBaseType? percentHint)
        {
            _exponents = exponents;
            _percentHint = percentHint;
        }

        internal static CssNumericType Empty() => new(new Int32[BaseTypeCount], null);

        /// <summary>
        /// Creates the type of a (normalized and known) unit.
        /// </summary>
        internal static CssNumericType ForUnit(String unit)
        {
            var type = Empty();
            var baseType = CssUnits.GetBaseType(unit);

            if (baseType.HasValue)
            {
                type._exponents[(Int32)baseType.Value] = 1;
            }

            return type;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the exponent of the length base type, if any.
        /// </summary>
        [DomName("length")]
        public Int32? Length => Get(CssNumericBaseType.Length);

        /// <summary>
        /// Gets the exponent of the angle base type, if any.
        /// </summary>
        [DomName("angle")]
        public Int32? Angle => Get(CssNumericBaseType.Angle);

        /// <summary>
        /// Gets the exponent of the time base type, if any.
        /// </summary>
        [DomName("time")]
        public Int32? Time => Get(CssNumericBaseType.Time);

        /// <summary>
        /// Gets the exponent of the frequency base type, if any.
        /// </summary>
        [DomName("frequency")]
        public Int32? Frequency => Get(CssNumericBaseType.Frequency);

        /// <summary>
        /// Gets the exponent of the resolution base type, if any.
        /// </summary>
        [DomName("resolution")]
        public Int32? Resolution => Get(CssNumericBaseType.Resolution);

        /// <summary>
        /// Gets the exponent of the flex base type, if any.
        /// </summary>
        [DomName("flex")]
        public Int32? Flex => Get(CssNumericBaseType.Flex);

        /// <summary>
        /// Gets the exponent of the percent base type, if any.
        /// </summary>
        [DomName("percent")]
        public Int32? Percent => Get(CssNumericBaseType.Percent);

        /// <summary>
        /// Gets the base type percentages resolve against, if any.
        /// </summary>
        [DomName("percentHint")]
        public CssNumericBaseType? PercentHint => _percentHint;

        #endregion

        #region Methods

        /// <summary>
        /// Gets the exponent of the given base type, if it is not zero.
        /// </summary>
        /// <param name="baseType">The base type to look up.</param>
        /// <returns>The exponent or null.</returns>
        public Int32? Get(CssNumericBaseType baseType)
        {
            var exponent = _exponents[(Int32)baseType];
            return exponent != 0 ? exponent : null;
        }

        /// <summary>
        /// Checks if the type describes a plain number.
        /// </summary>
        internal Boolean IsNumber => _percentHint is null && IsEmpty();

        /// <summary>
        /// Checks if the type matches the given base type with an exponent of 1, e.g.,
        /// &lt;length&gt; or &lt;angle&gt;.
        /// </summary>
        internal Boolean Matches(CssNumericBaseType baseType) =>
            Matches(baseType, ignoreHint: baseType == CssNumericBaseType.Percent);

        /// <summary>
        /// Checks if the type matches a &lt;length-percentage&gt;, which includes a length
        /// combined with a percentage, e.g., calc(1px + 10%).
        /// </summary>
        internal Boolean MatchesLengthPercentage() =>
            Matches(CssNumericBaseType.Percent) ||
            Matches(CssNumericBaseType.Length, ignoreHint: _percentHint is null || _percentHint == CssNumericBaseType.Length);

        private Boolean Matches(CssNumericBaseType baseType, Boolean ignoreHint)
        {
            for (var i = 0; i < BaseTypeCount; i++)
            {
                if (_exponents[i] != (i == (Int32)baseType ? 1 : 0))
                {
                    return false;
                }
            }

            return ignoreHint || _percentHint is null;
        }

        /// <summary>
        /// Adds two types.
        /// https://drafts.css-houdini.org/css-typed-om-1/#cssnumericvalue-add-two-types
        /// </summary>
        /// <returns>The resulting type or null in case of a failure.</returns>
        internal static CssNumericType? Add(CssNumericType first, CssNumericType second)
        {
            var a = first.Clone();
            var b = second.Clone();

            if (a._percentHint.HasValue && b._percentHint.HasValue && a._percentHint != b._percentHint)
            {
                return null;
            }
            else if (a._percentHint.HasValue)
            {
                b.ApplyPercentHint(a._percentHint.Value);
            }
            else if (b._percentHint.HasValue)
            {
                a.ApplyPercentHint(b._percentHint.Value);
            }

            if (a.HasSameExponents(b))
            {
                return a;
            }

            var hasPercent = a.Has(CssNumericBaseType.Percent) || b.Has(CssNumericBaseType.Percent);
            var hasOther = a.HasOtherThanPercent() || b.HasOtherThanPercent();

            if (hasPercent && hasOther)
            {
                for (var i = 0; i < BaseTypeCount; i++)
                {
                    var hint = (CssNumericBaseType)i;

                    if (hint != CssNumericBaseType.Percent)
                    {
                        var x = a.Clone();
                        var y = b.Clone();
                        x.ApplyPercentHint(hint);
                        y.ApplyPercentHint(hint);

                        if (x.HasSameExponents(y))
                        {
                            return x;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Multiplies two types.
        /// https://drafts.css-houdini.org/css-typed-om-1/#cssnumericvalue-multiply-two-types
        /// </summary>
        /// <returns>The resulting type or null in case of a failure.</returns>
        internal static CssNumericType? Multiply(CssNumericType first, CssNumericType second)
        {
            var a = first.Clone();
            var b = second.Clone();

            if (a._percentHint.HasValue && b._percentHint.HasValue && a._percentHint != b._percentHint)
            {
                return null;
            }
            else if (a._percentHint.HasValue)
            {
                b.ApplyPercentHint(a._percentHint.Value);
            }
            else if (b._percentHint.HasValue)
            {
                a.ApplyPercentHint(b._percentHint.Value);
            }

            for (var i = 0; i < BaseTypeCount; i++)
            {
                a._exponents[i] += b._exponents[i];
            }

            return a;
        }

        /// <summary>
        /// Inverts the type, i.e., negates all exponents.
        /// </summary>
        internal CssNumericType Invert()
        {
            var result = Clone();

            for (var i = 0; i < BaseTypeCount; i++)
            {
                result._exponents[i] = -result._exponents[i];
            }

            return result;
        }

        /// <summary>
        /// Checks if both types are equal.
        /// </summary>
        /// <param name="other">The type to compare to.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssNumericType? other) =>
            other is not null && other._percentHint == _percentHint && HasSameExponents(other);

        /// <summary>
        /// Checks if both types are equal.
        /// </summary>
        public override Boolean Equals(Object? obj) => Equals(obj as CssNumericType);

        /// <summary>
        /// Gets the hash code of the type.
        /// </summary>
        public override Int32 GetHashCode()
        {
            var hash = _percentHint.HasValue ? (Int32)_percentHint.Value + 1 : 0;

            for (var i = 0; i < BaseTypeCount; i++)
            {
                hash = hash * 31 + _exponents[i];
            }

            return hash;
        }

        #endregion

        #region Helpers

        private CssNumericType Clone() => new((Int32[])_exponents.Clone(), _percentHint);

        private Boolean Has(CssNumericBaseType baseType) => _exponents[(Int32)baseType] != 0;

        private Boolean IsEmpty()
        {
            for (var i = 0; i < BaseTypeCount; i++)
            {
                if (_exponents[i] != 0)
                {
                    return false;
                }
            }

            return true;
        }

        private Boolean HasOtherThanPercent()
        {
            for (var i = 0; i < BaseTypeCount; i++)
            {
                if (i != (Int32)CssNumericBaseType.Percent && _exponents[i] != 0)
                {
                    return true;
                }
            }

            return false;
        }

        private Boolean HasSameExponents(CssNumericType other)
        {
            for (var i = 0; i < BaseTypeCount; i++)
            {
                if (_exponents[i] != other._exponents[i])
                {
                    return false;
                }
            }

            return true;
        }

        private void ApplyPercentHint(CssNumericBaseType hint)
        {
            var percent = (Int32)CssNumericBaseType.Percent;

            if (hint != CssNumericBaseType.Percent)
            {
                _exponents[(Int32)hint] += _exponents[percent];
                _exponents[percent] = 0;
            }

            _percentHint = hint;
        }

        #endregion
    }
}
