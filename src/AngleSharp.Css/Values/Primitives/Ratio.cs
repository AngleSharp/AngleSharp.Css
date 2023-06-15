namespace AngleSharp.Css.Values
{
    using System;

    /// <summary>
    /// Represents a ratio (top to bottom) value.
    /// </summary>
    public struct Ratio : IEquatable<Ratio>, IComparable<Ratio>, ICssPrimitiveValue
    {
        #region Fields

        private readonly Double _top;
        private readonly Double _bottom;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new ratio value.
        /// </summary>
        /// <param name="top">The first number.</param>
        /// <param name="bottom">The second number.</param>
        public Ratio(Double top, Double bottom)
        {
            _top = top;
            _bottom = bottom;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => String.Concat(_top.CssStringify(), "/", _bottom.CssStringify());

        /// <summary>
        /// Gets the first number of the ratio.
        /// </summary>
        public Double Top => _top;

        /// <summary>
        /// Gets the second number of the ratio.
        /// </summary>
        public Double Bottom => _bottom;

        /// <summary>
        /// Gets the normalized (Top / Bottom) value.
        /// </summary>
        public Double Value => _top / _bottom;

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current resolution equals the given one.
        /// </summary>
        /// <param name="other">The given resolution to check for equality.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(Ratio other) => Value == other.Value;

        #endregion

        #region Equality

        /// <summary>
        /// Compares the current fraction against the given one.
        /// </summary>
        /// <param name="other">The fraction to compare to.</param>
        /// <returns>The result of the comparison.</returns>
        public Int32 CompareTo(Ratio other) => Value.CompareTo(other.Value);

        /// <summary>
        /// Tests if another object is equal to this object.
        /// </summary>
        /// <param name="obj">The object to test with.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj)
        {
            var other = obj as Ratio?;

            if (other != null)
            {
                return Equals(other.Value);
            }

            return false;
        }

        /// <summary>
        /// Returns a hash code that defines the current fraction.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode() => Value.GetHashCode();

        #endregion
    }
}
