#nullable disable
namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a border radius value.
    /// </summary>
    sealed class CssBorderRadiusValue : ICssCompositeValue, IEquatable<CssBorderRadiusValue>
    {
        #region Fields

        private readonly CssPeriodicValue _horizontal;
        private readonly CssPeriodicValue _vertical;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new border radius value.
        /// </summary>
        public CssBorderRadiusValue(CssPeriodicValue horizontal, CssPeriodicValue vertical)
        {
            _horizontal = horizontal;
            _vertical = vertical;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the horizontal values.
        /// </summary>
        public CssPeriodicValue Horizontal => _horizontal;

        /// <summary>
        /// Gets the vertical values.
        /// </summary>
        public CssPeriodicValue Vertical => _vertical;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var h = _horizontal.CssText;

                if (!Object.ReferenceEquals(_horizontal, _vertical))
                {
                    var v = _vertical.CssText;

                    if (!String.IsNullOrEmpty(v) && h != v)
                    {
                        return String.Concat(h, " / ", v);
                    }
                }

                return h;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssBorderRadiusValue other)
        {
            if (other is not null)
            {
                var comparer = EqualityComparer<ICssValue>.Default;
                return comparer.Equals(_horizontal, other._horizontal) && comparer.Equals(_vertical, other._vertical);
            }

            return false;
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssBorderRadiusValue value && Equals(value);

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var h = ComputePeriodic(_horizontal, context, RenderMode.Horizontal);
            var v = ComputePeriodic(_vertical, context, RenderMode.Vertical);
            return new CssBorderRadiusValue((CssPeriodicValue)h, (CssPeriodicValue)v);
        }

        private static CssPeriodicValue ComputePeriodic(CssPeriodicValue value, ICssComputeContext context, RenderMode mode) =>
            new CssPeriodicValue(new[]
            {
                ComputeLength(value.Top, context, mode),
                ComputeLength(value.Right, context, mode),
                ComputeLength(value.Bottom, context, mode),
                ComputeLength(value.Left, context, mode),
            });

        private static ICssValue ComputeLength(ICssValue value, ICssComputeContext context, RenderMode mode)
        {
            if (value is CssLengthValue length && length.Type == CssLengthValue.Unit.Percent)
            {
                return new CssLengthValue(length.Value * 0.01 * GetDimension(context, mode), CssLengthValue.Unit.Px);
            }

            if (value is CssPercentageValue percentage)
            {
                var dimension = GetDimension(context, mode);
                return new CssLengthValue(percentage.Value * 0.01 * dimension, CssLengthValue.Unit.Px);
            }

            return value.Compute(context);
        }

        private static Double GetDimension(ICssComputeContext context, RenderMode mode)
        {
            var name = mode == RenderMode.Horizontal ? PropertyNames.Width : PropertyNames.Height;
            var properties = (context as ILocalComputeContext)?.Properties;
            var property = properties?.GetProperty(name);

            if (property?.RawValue is CssLengthValue length && length.Type == CssLengthValue.Unit.Px)
            {
                return length.Value;
            }

            if (CssLengthValue.TryParse(properties?.GetPropertyValue(name), out var parsed))
            {
                return parsed.ToPixel(context.Device);
            }

            return mode == RenderMode.Horizontal ? context.Device.RenderWidth : context.Device.RenderHeight;
        }

        #endregion
    }
}
