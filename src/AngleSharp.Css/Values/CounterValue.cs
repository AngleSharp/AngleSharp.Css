namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Sets a CSS counter.
    /// </summary>
    public struct CounterValue : ICssValue
    {
        #region Fields

        private readonly String _name;
        private readonly Int32 _value;

        #endregion

        #region ctor

        /// <summary>
        /// Specifies a counter value.
        /// </summary>
        /// <param name="name">The name of the referenced counter.</param>
        /// <param name="value">The new value of the counter.</param>
        public CounterValue(String name, Int32 value)
        {
            _name = name;
            _value = value;
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

        /// <summary>
        /// Gets the identifier of the counter.
        /// </summary>
        public String Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets the value of the counter.
        /// </summary>
        public Int32 Value
        {
            get { return _value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Serializes the counter value.
        /// </summary>
        public override String ToString()
        {
            return String.Concat(_name, " ", _value.ToString());
        }

        #endregion
    }
}
