namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS content function call.
    /// </summary>
    /// <remarks>
    /// Creates a new content function call.
    /// </remarks>
    /// <param name="type">The used dimension argument.</param>
    public sealed class CssContentValue(String type) : ICssFunctionValue, IEquatable<CssContentValue>
    {
        #region Fields

        private readonly String _type = type;

        #endregion
        
        #region ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets the used type.
        /// </summary>
        public String Type => _type;

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name => FunctionNames.Content;

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments => new ICssValue[] { new CssIdentifierValue(_type) };

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => Name.CssFunction(_type);

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssContentValue other) => _type.Equals(other._type);

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssContentValue value && Equals(value);

        ICssValue ICssValue.Compute(ICssComputeContext context) => this;

        #endregion
    }
}
