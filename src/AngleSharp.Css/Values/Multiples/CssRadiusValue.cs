namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a periodic CSS value.
    /// </summary>
    public class CssRadiusValue<T> : ICssMultipleValue
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
                switch (index)
                {
                    case 0:
                        return Width;
                    case 1:
                        return Height;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index));
                }
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

        IEnumerator<ICssValue> IEnumerable<ICssValue>.GetEnumerator()
        {
            yield return Width;
            yield return Height;
        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<ICssValue>)this).GetEnumerator();

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
