namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a CSS value list.
    /// </summary>
    public class CssListValue<T> : ICssMultipleValue, IEquatable<CssListValue<T>>
        where T : ICssValue
    {
        #region Fields

        private readonly T[] _items;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new list value.
        /// </summary>
        /// <param name="items">The items to contain.</param>
        public CssListValue(T[] items = null)
        {
            _items = items ?? Array.Empty<T>();
        }

        #endregion

        #region Properties

        /// <inheritdoc />
        public ICssValue this[Int32 index] => _items[index];

        /// <inheritdoc />
        public T[] Items => _items;

        /// <inheritdoc />
        public String CssText => _items.Join(", ");

        /// <inheritdoc />
        public Int32 Count => _items.Length;

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssListValue<T> other)
        {
            var count = _items.Length;

            if (count == other._items.Length)
            {
                for (var i = 0; i < count; i++)
                {
                    var a = _items[i];
                    var b = other._items[i];

                    if (!a.Equals(b))
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        IEnumerator<ICssValue> IEnumerable<ICssValue>.GetEnumerator() =>
            _items.OfType<ICssValue>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var items = _items.Select(v => (T)v.Compute(context)).ToArray();
            return new CssListValue<T>(items);
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssListValue<T> value && Equals(value);

        #endregion
    }

    /// <summary>
    /// Represents a CSS value list.
    /// </summary>
    sealed class CssListValue : CssListValue<ICssValue>
    {
        #region ctor

        public CssListValue(ICssValue[] items = null)
            : base(items)
        {
        }

        #endregion
    }
}
