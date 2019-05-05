namespace AngleSharp.Css.Values
{
    using System;

    /// <summary>
    /// Sets a CSS counter.
    /// </summary>
    struct CounterValue : ICssPrimitiveValue
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
        public String CssText => String.Concat(_name, " ", _value.ToString());

        /// <summary>
        /// Gets the identifier of the counter.
        /// </summary>
        public String Name => _name;

        /// <summary>
        /// Gets the value of the counter.
        /// </summary>
        public Int32 Value => _value;

        #endregion
    }
}
