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
    public class CssRadiusValue<T> : ICssMultipleValue, IEquatable<CssRadiusValue<T>>
        where T : ICssValue
    {
        #region Fields

        private readonly T[] _values;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS radius value container.
        /// </summary>
        /// <param name="values">The items to contain.</param>
        public CssRadiusValue(T[] values = null)
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
                    0 => Width,
                    1 => (ICssValue)Height,
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

                if (l == 2 && parts[1].Is(parts[0]))
                {
                    l = 1;
                    parts = new[] { parts[0] };
                }

                return String.Join(" ", parts);
            }
        }

        /// <inheritdoc />
        public T Width => _values.Length > 0 ? _values[0] : default;

        /// <inheritdoc />
        public T Height => _values.Length > 1 ? _values[1] : Width;

        /// <inheritdoc />
        public Int32 Count => 2;

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssRadiusValue<T> other)
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
            yield return Width;
            yield return Height;
        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<ICssValue>)this).GetEnumerator();

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var values = _values.Select(v => (T)v.Compute(context)).ToArray();
            return new CssRadiusValue<T>(values);
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssRadiusValue<T> value && Equals(value);

        #endregion
    }

    /// <summary>
    /// Represents a radius (w, h) CSS value.
    /// </summary>
    sealed class CssRadiusValue : CssRadiusValue<ICssValue>
    {
        #region ctor

        public CssRadiusValue(ICssValue[] values = null)
            : base(values)
        {
        }

        #endregion
    }
}
