namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a periodic CSS value.
    /// </summary>
    class Periodic<T> : ICssValue
        where T : ICssValue
    {
        #region Fields

        private readonly T[] _values;

        #endregion

        #region ctor

        /// <summary>
        /// Constructs a new periodic value from the given values.
        /// </summary>
        public Periodic(T[] values)
        {
            _values = values;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
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

        /// <summary>
        /// Gets the first value.
        /// </summary>
        public T Top
        {
            get { return _values.Length > 0 ? _values[0] : default(T); }
        }

        /// <summary>
        /// Gets the second value.
        /// </summary>
        public T Right
        {
            get { return _values.Length > 1 ? _values[1] : Top; }
        }

        /// <summary>
        /// Gets the third value.
        /// </summary>
        public T Bottom
        {
            get { return _values.Length > 2 ? _values[2] : Top; }
        }

        /// <summary>
        /// Gets the fourth value.
        /// </summary>
        public T Left
        {
            get { return _values.Length > 3 ? _values[3] : Right; }
        }

        #endregion
    }
}
