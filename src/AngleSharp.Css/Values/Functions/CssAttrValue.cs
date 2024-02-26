namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS attr function call.
    /// </summary>
    public sealed class CssAttrValue : ICssFunctionValue, IEquatable<CssAttrValue>
    {
        #region Fields

        private readonly String _attribute;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new attr function call.
        /// </summary>
        /// <param name="attribute">The referenced attribute name.</param>
        public CssAttrValue(String attribute)
        {
            _attribute = attribute;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the used attribute.
        /// </summary>
        public String Attribute => _attribute;

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name => FunctionNames.Attr;

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments => new ICssValue[] { new CssIdentifierValue(_attribute) };

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => Name.CssFunction(_attribute);

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssAttrValue other) => other is not null && _attribute == other._attribute;

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssAttrValue value && Equals(value);

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            return this;
        }

        #endregion
    }
}
