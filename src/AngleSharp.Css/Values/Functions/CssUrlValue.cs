namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents an URL object.
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/uri
    /// </summary>
    /// <remarks>
    /// Creates a new URL reference for the given path.
    /// </remarks>
    /// <param name="path">The path to reference.</param>
    public sealed class CssUrlValue(String path) : ICssImageValue, ICssFunctionValue, IEquatable<CssUrlValue>
    {
        #region Fields

        private readonly String _path = path;

        #endregion
        
        #region ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name => FunctionNames.Url;

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments => new ICssValue[] { new CssStringValue(_path) };

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => Name.CssFunction(_path.CssString());

        /// <summary>
        /// Gets the referenced path.
        /// </summary>
        public String Path => _path;

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssUrlValue? other) => other is not null && _path.Equals(other._path);

        Boolean IEquatable<ICssValue>.Equals(ICssValue? other) => other is CssUrlValue value && Equals(value);

        ICssValue ICssValue.Compute(ICssComputeContext context) => this;

        #endregion
    }
}
