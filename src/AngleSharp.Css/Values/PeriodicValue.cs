namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    public sealed class PeriodicValue<T> : ICssValue
        where T : ICssValue
    {
        #region Fields

        private readonly T[] _values;

        #endregion

        #region ctor

        public PeriodicValue(T[] values)
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
            get { return ToString(); }
        }

        public T Top
        {
            get { return _values.Length > 0 ? _values[0] : default(T); }
        }

        public T Right
        {
            get { return _values.Length > 1 ? _values[1] : Top; }
        }

        public T Bottom
        {
            get { return _values.Length > 2 ? _values[2] : Top; }
        }

        public T Left
        {
            get { return _values.Length > 3 ? _values[3] : Right; }
        }

        #endregion

        #region Methods

        public override String ToString()
        {
            var l = _values.Length;
            var parts = new String[l];

            for (var i = 0; i < l; i++)
            {
                parts[i] = _values[i].CssText;
            }

            if (l == 2 && parts[1] == parts[0])
            {
                parts = new[] { parts[0] };
            }

            return String.Join(" ", parts);
        }

        #endregion
    }
}
