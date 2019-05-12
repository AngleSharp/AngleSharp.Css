namespace AngleSharp.Css.Values
{
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Sets a CSS counter.
    /// </summary>
    struct CounterValue : ICssPrimitiveValue, IEquatable<CounterValue>
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

        #region Methods

        /// <summary>
        /// Checks the two counter values for equality.
        /// </summary>
        /// <param name="other">The other counter to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CounterValue other) => Name.Is(other.Name) && Value == other.Value;

        /// <summary>
        /// Checks for equality against the given object,
        /// if the provided object is no counter vlaue the
        /// result is false.
        /// </summary>
        /// <param name="obj">The object to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj) =>
            obj is CounterValue cv ? Equals(cv) : false;

        /// <summary>
        /// Gets the hash code of the object.
        /// </summary>
        /// <returns>The computed hash code.</returns>
        public override Int32 GetHashCode() => CssText.GetHashCode();

        #endregion
    }
}
