namespace AngleSharp.Css.Values
{
    using System;

    /// <summary>
    /// Represents a selected CSS enum value.
    /// </summary>
    struct Constant<T> : ICssPrimitiveValue, IEquatable<Constant<T>>
    {
        #region Fields

        private readonly String _key;
        private readonly T _data;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new selected CSS enum value.
        /// </summary>
        /// <param name="key">The key representation.</param>
        /// <param name="data">The associated data.</param>
        public Constant(String key, T data)
        {
            _key = key;
            _data = data;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the associated value.
        /// </summary>
        public T Value => _data;

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
        public Boolean Equals(Constant<T> other) => Object.Equals(other.Value, Value);

        /// <summary>
        /// Checks for equality against the given object.
        /// Only matches if the provided object is also a constant.
        /// </summary>
        /// <param name="obj">The object to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj) =>
            obj is Constant<T> constant ? Equals(constant) : false;

        /// <summary>
        /// Gets the computed hash code of the constant.
        /// </summary>
        /// <returns>The hash code of the object.</returns>
        public override Int32 GetHashCode() => _data.GetHashCode();

        #endregion
    }
}
