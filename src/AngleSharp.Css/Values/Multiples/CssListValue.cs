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
    /// <remarks>
    /// Creates a new list value.
    /// </remarks>
    /// <param name="items">The items to contain.</param>
    public class CssListValue(ICssValue[]? items = null) : ICssMultipleValue, IEquatable<ICssValue>
    {
        #region Fields

        private readonly ICssValue[] _items = items ?? [];

        #endregion

        #region Properties

        /// <inheritdoc />
        public ICssValue this[Int32 index] => _items[index];

        /// <inheritdoc />
        public ICssValue[] Items => _items;

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
        public Boolean Equals(CssListValue? other)
        {
            if (other is not null)
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
            }

            return false;
        }

        IEnumerator<ICssValue?> IEnumerable<ICssValue?>.GetEnumerator() =>
            _items.OfType<ICssValue>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();

        ICssValue? ICssValue.Compute(ICssComputeContext context)
        {
            var items = _items.Select(v => v.Compute(context)).NotNull().ToArray();

            if (items.Length == _items.Length)
            {
                return new CssListValue(items);
            }

            return null;
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue? other) => other is CssListValue value && Equals(value);

        #endregion
    }
}
