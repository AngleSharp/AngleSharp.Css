namespace AngleSharp.Css.Values
{
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a CSS line names definition.
    /// </summary>
    struct LineNames : ICssPrimitiveValue, IEquatable<LineNames>
    {
        #region Fields

        private readonly String[] _names;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new line names definition.
        /// </summary>
        /// <param name="names">The names to contain.</param>
        public LineNames(IEnumerable<String> names)
        {
            _names = names.ToArray();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the contained line names.
        /// </summary>
        public String[] Names => _names;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => $"[{String.Join(" ", _names)}]";

        #endregion

        #region Methods

        /// <summary>
        /// Checks the two line names for equality.
        /// </summary>
        /// <param name="other">The other names to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(LineNames other) =>
            Names.Length == other.Names.Length &&
            Names.Zip(other.Names, (a, b) => a.Is(b)).All(matches => matches);

        /// <summary>
        /// Checks for equality against the given object, if
        /// the provided object are no line names the result is
        /// false.
        /// </summary>
        /// <param name="obj">The object to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj) =>
            obj is LineNames names ? Equals(names) : false;

        /// <summary>
        /// Gets the hash code of the object.
        /// </summary>
        /// <returns>The computed hash code.</returns>
        public override Int32 GetHashCode() => CssText.GetHashCode();

        #endregion
    }
}
