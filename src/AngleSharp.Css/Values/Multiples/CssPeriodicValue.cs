namespace AngleSharp.Css.Values
{
    using AngleSharp.Css;
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a periodic CSS value.
    /// </summary>
    /// <remarks>
    /// Creates a new CSS periodic value container.
    /// </remarks>
    /// <param name="values">The items to contain.</param>
    public class CssPeriodicValue(ICssValue[]? values = null) : ICssMultipleValue, IEquatable<CssPeriodicValue>
    {
        #region Fields

        private readonly ICssValue[] _values = values ?? [];

        #endregion

        #region Properties

        /// <inheritdoc />
        public ICssValue? this[Int32 index]
        {
            get
            {
                return index switch
                {
                    0 => Top,
                    1 => Right,
                    2 => Bottom,
                    3 => Left,
                    _ => throw new ArgumentOutOfRangeException(nameof(index)),
                };
            }
        }

        /// <inheritdoc />
        public String CssText
        {
            get
            {
                var l = _values.Length;
                var parts = new String[l];

                for (var i = 0; i < l; i++)
                {
                    parts[i] = _values[i].CssText;
                }

                if (l == 4 && parts[3].Is(parts[1]))
                {
                    l = 3;
                    parts = [parts[0], parts[1], parts[2]];
                }

                if (l == 3 && parts[2].Is(parts[0]))
                {
                    l = 2;
                    parts = [parts[0], parts[1]];
                }

                if (l == 2 && parts[1].Is(parts[0]))
                {
                    l = 1;
                    parts = [parts[0]];
                }

                return String.Join(" ", parts);
            }
        }

        /// <inheritdoc />
        public ICssValue? Top => _values.Length > 0 ? _values[0] : default;

        /// <inheritdoc />
        public ICssValue? Right => _values.Length > 1 ? _values[1] : Top;

        /// <inheritdoc />
        public ICssValue? Bottom => _values.Length > 2 ? _values[2] : Top;

        /// <inheritdoc />
        public ICssValue? Left => _values.Length > 3 ? _values[3] : Right;

        /// <inheritdoc />
        public Int32 Count => 4;

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssPeriodicValue? other)
        {
            if (other is not null)
            {
                var count = _values.Length;

                if (count == other._values.Length)
                {
                    for (var i = 0; i < count; i++)
                    {
                        var a = _values[i];
                        var b = other._values[i];

                        if (!a.Equals(b))
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        IEnumerator<ICssValue?> IEnumerable<ICssValue?>.GetEnumerator()
        {
            yield return Top;
            yield return Right;
            yield return Bottom;
            yield return Left;
        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<ICssValue>)this).GetEnumerator();

        ICssValue? ICssValue.Compute(ICssComputeContext context)
        {
            var values = _values.Select(v => v.Compute(context)).NotNull().ToArray();

            if (values.Length ==  _values.Length)
            {
                return new CssPeriodicValue(values);
            }

            return null;
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue? other) => other is CssPeriodicValue value && Equals(value);

        #endregion
    }
}
