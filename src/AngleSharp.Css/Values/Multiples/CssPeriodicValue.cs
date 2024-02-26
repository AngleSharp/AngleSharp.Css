namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a periodic CSS value.
    /// </summary>
    public class CssPeriodicValue<T> : ICssMultipleValue, IEquatable<CssPeriodicValue<T>>
        where T : ICssValue
    {
        #region Fields

        private readonly T[] _values;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS periodic value container.
        /// </summary>
        /// <param name="values">The items to contain.</param>
        public CssPeriodicValue(T[] values = null)
        {
            _values = values ?? Array.Empty<T>();
        }

        #endregion

        #region Properties

        /// <inheritdoc />
        public ICssValue this[Int32 index]
        {
            get
            {
                return index switch
                {
                    0 => Top,
                    1 => Right,
                    2 => Bottom,
                    3 => (ICssValue)Left,
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
                    parts = new[] { parts[0], parts[1], parts[2] };
                }

                if (l == 3 && parts[2].Is(parts[0]))
                {
                    l = 2;
                    parts = new[] { parts[0], parts[1] };
                }

                if (l == 2 && parts[1].Is(parts[0]))
                {
                    l = 1;
                    parts = new[] { parts[0] };
                }

                return String.Join(" ", parts);
            }
        }

        /// <inheritdoc />
        public T Top => _values.Length > 0 ? _values[0] : default;

        /// <inheritdoc />
        public T Right => _values.Length > 1 ? _values[1] : Top;

        /// <inheritdoc />
        public T Bottom => _values.Length > 2 ? _values[2] : Top;

        /// <inheritdoc />
        public T Left => _values.Length > 3 ? _values[3] : Right;

        /// <inheritdoc />
        public Int32 Count => 4;

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssPeriodicValue<T> other)
        {
            if (other is not null)
            {
                var comparer = EqualityComparer<ICssValue>.Default;
                var count = _values.Length;

                if (count == other._values.Length)
                {
                    for (var i = 0; i < count; i++)
                    {
                        var a = _values[i];
                        var b = other._values[i];

                        if (!comparer.Equals(a, b))
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        IEnumerator<ICssValue> IEnumerable<ICssValue>.GetEnumerator()
        {
            yield return Top;
            yield return Right;
            yield return Bottom;
            yield return Left;
        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<ICssValue>)this).GetEnumerator();

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var values = _values.Select(v => (T)v.Compute(context)).ToArray();
            return new CssPeriodicValue<T>(values);
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssPeriodicValue<T> value && Equals(value);

        #endregion
    }

    /// <summary>
    /// Represents a periodic CSS value.
    /// </summary>
    sealed class CssPeriodicValue : CssPeriodicValue<ICssValue>
    {
        #region ctor

        public CssPeriodicValue(ICssValue[] values = null)
            : base(values)
        {
        }

        #endregion
    }
}
