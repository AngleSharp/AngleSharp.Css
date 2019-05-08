namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a periodic CSS value.
    /// </summary>
    class CssPeriodicValue<T> : ICssMultipleValue
        where T : ICssValue
    {
        #region Fields

        private readonly T[] _values;

        #endregion

        #region ctor
        
        public CssPeriodicValue(T[] values)
        {
            _values = values;
        }

        #endregion

        #region Properties

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

                if (l == 4 && parts[3] == parts[1])
                {
                    l = 3;
                    parts = new[] { parts[0], parts[1], parts[2] };
                }

                if (l == 3 && parts[2] == parts[0])
                {
                    l = 2;
                    parts = new[] { parts[0], parts[1] };
                }

                if (l == 2 && parts[1] == parts[0])
                {
                    l = 1;
                    parts = new[] { parts[0] };
                }

                return String.Join(" ", parts);
            }
        }

        public T Top => _values.Length > 0 ? _values[0] : default(T);

        public T Right => _values.Length > 1 ? _values[1] : Top;

        public T Bottom => _values.Length > 2 ? _values[2] : Top;

        public T Left => _values.Length > 3 ? _values[3] : Right;

        public Int32 Count => 4;

        #endregion

        #region Methods

        IEnumerator<ICssValue> IEnumerable<ICssValue>.GetEnumerator() =>
            _values.OfType<ICssValue>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _values.GetEnumerator();

        #endregion
    }

    /// <summary>
    /// Represents a periodic CSS value.
    /// </summary>
    class CssPeriodicValue : CssPeriodicValue<ICssValue>
    {
        #region ctor

        public CssPeriodicValue(ICssValue[] values)
            : base(values)
        {
        }

        #endregion
    }
}
