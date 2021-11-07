namespace AngleSharp.Css.Values
{
    using System;

    /// <summary>
    /// Represents a selected CSS enum value.
    /// </summary>
    struct Constant<T,U> : ICssPrimitiveValue, IEquatable<Constant<T,U>>
    {
        #region Fields

        private readonly String _key;
        private readonly T _dataT;
        private readonly U _dataU;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new selected CSS enum value.
        /// </summary>
        /// <param name="key">The key representation.</param>
        /// <param name="dataT">The associated data.</param>
        /// <param name="dataU">The associated data.</param>
        public Constant(String key, T dataT, U dataU)
        {
            _key = key;
            _dataT = dataT;
            _dataU = dataU;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the associated value.
        /// </summary>
        public T Value1 => _dataT;

        /// <summary>
        /// Gets the associated value.
        /// </summary>
        public U Value2 => _dataU;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => _key;

        #endregion

        #region Methods

        /// <summary>
        /// Checks for equality against the given constant.
        /// </summary>
        /// <param name="other">The other constant to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(Constant<T,U> other) => Object.Equals(other.Value1, Value1) && Object.Equals(other.Value2, Value2);

        /// <summary>
        /// Checks for equality against the given object.
        /// Only matches if the provided object is also a constant.
        /// </summary>
        /// <param name="obj">The object to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj) =>
            obj is Constant<T,U> constant ? Equals(constant) : false;

        /// <summary>
        /// Gets the computed hash code of the constant.
        /// </summary>
        /// <returns>The hash code of the object.</returns>
        public override Int32 GetHashCode() => _dataT.GetHashCode() ^ _dataU.GetHashCode();

        #endregion
    }
}
