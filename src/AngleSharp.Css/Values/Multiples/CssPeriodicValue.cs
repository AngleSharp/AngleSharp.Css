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
    public class CssPeriodicValue<T> : ICssMultipleValue
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
                switch (index)
                {
                    case 0:
                        return Top;
                    case 1:
                        return Right;
                    case 2:
                        return Bottom;
                    case 3:
                        return Left;
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

        IEnumerator<ICssValue> IEnumerable<ICssValue>.GetEnumerator()
        {
            yield return Top;
            yield return Right;
            yield return Bottom;
            yield return Left;
        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<ICssValue>)this).GetEnumerator();

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
