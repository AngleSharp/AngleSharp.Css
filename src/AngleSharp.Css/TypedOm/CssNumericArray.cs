namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// A read-only list of numeric values, e.g., the arguments of a math value.
    /// https://www.w3.org/TR/css-typed-om-1/#cssnumericarray
    /// </summary>
    [DomName("CSSNumericArray")]
    public sealed class CssNumericArray : IReadOnlyList<CssNumericValue>
    {
        private readonly CssNumericValue[] _values;

        internal CssNumericArray(CssNumericValue[] values)
        {
            _values = values;
        }

        /// <summary>
        /// Gets the value at the given index.
        /// </summary>
        [DomName("item")]
        [DomAccessor(Accessors.Getter)]
        public CssNumericValue this[Int32 index] => _values[index];

        /// <summary>
        /// Gets the number of values.
        /// </summary>
        [DomName("length")]
        public Int32 Length => _values.Length;

        Int32 IReadOnlyCollection<CssNumericValue>.Count => _values.Length;

        /// <summary>
        /// Gets an enumerator over the values.
        /// </summary>
        public IEnumerator<CssNumericValue> GetEnumerator() => ((IEnumerable<CssNumericValue>)_values).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
