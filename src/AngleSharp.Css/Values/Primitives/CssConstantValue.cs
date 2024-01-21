namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a selected CSS enum value.
    /// </summary>
    /// <param name="key">The key representation.</param>
    /// <param name="data">The associated data.</param>
    public readonly struct CssConstantValue<T>(String key, T? data) : ICssPrimitiveValue, IEquatable<CssConstantValue<T>>
    {
        #region Fields

        private readonly String _key = key;
        private readonly T? _data = data;

        #endregion
        
        #region ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets the associated value.
        /// </summary>
        public T? Value => _data;

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
        public Boolean Equals(CssConstantValue<T> other) => Object.Equals(other.Value, Value);

        /// <summary>
        /// Checks for equality against the given object.
        /// Only matches if the provided object is also a constant.
        /// </summary>
        /// <param name="obj">The object to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public override Boolean Equals(Object? obj) =>
            obj is CssConstantValue<T> constant && Equals(constant);

        Boolean IEquatable<ICssValue>.Equals(ICssValue? other) => other is CssConstantValue<T> value && Equals(value);

        /// <summary>
        /// Gets the computed hash code of the constant.
        /// </summary>
        /// <returns>The hash code of the object.</returns>
        public override Int32 GetHashCode() => _data?.GetHashCode() ?? 0;

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            return this;
        }

        /// <summary>
        /// Equality check.
        /// </summary>
        public static bool operator ==(CssConstantValue<T> left, CssConstantValue<T> right) => left.Equals(right);

        /// <summary>
        /// Inequality check.
        /// </summary>
        public static bool operator !=(CssConstantValue<T> left, CssConstantValue<T> right) => !(left == right);

        #endregion
    }
}
