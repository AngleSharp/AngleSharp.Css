namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a tuple of CSS values.
    /// </summary>
    /// <remarks>
    /// Creates a new CSS tuple value.
    /// </remarks>
    /// <param name="items">The items to contain.</param>
    /// <param name="separator">The custom separator, if any.</param>
    public class CssTupleValue(ICssValue[]? items = null, String? separator = null) : ICssMultipleValue, IEquatable<CssTupleValue>
    {
        #region Fields

        private readonly ICssValue[] _items = items ?? [];
        private readonly String _separator = separator ?? " ";

        #endregion

        #region ctor

        #endregion

        #region Properties

        /// <inheritdoc />
        public ICssValue this[Int32 index] => _items[index];

        /// <inheritdoc />
        public ICssValue[] Items => _items;

        /// <inheritdoc />
        public String Separator => _separator;

        /// <inheritdoc />
        public String CssText => _items
            .Where(m => m is CssInitialValue == false)
            .Join(_separator);

        /// <inheritdoc />
        public Int32 Count => _items.Length;

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssTupleValue? other)
        {
            if (other is not null)
            {
                var count = _items.Length;

                if (_separator.Equals(other._separator) && count == other._items.Length)
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

        IEnumerator<ICssValue> IEnumerable<ICssValue?>.GetEnumerator() =>
            _items.OfType<ICssValue>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();

        ICssValue? ICssValue.Compute(ICssComputeContext context)
        {
            var items = _items.Select(v => v.Compute(context)).NotNull().ToArray();

            if (items.Length == _items.Length)
            {
                return new CssTupleValue(items, _separator);
            }

            return null;
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue? other) => other is CssTupleValue value && Equals(value);

        #endregion
    }
}
