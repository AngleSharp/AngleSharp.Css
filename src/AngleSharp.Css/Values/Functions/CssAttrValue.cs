namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS attr function call.
    /// </summary>
    sealed class CssAttrValue : ICssFunctionValue
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
        public ICssValue[] Arguments => new ICssValue[] { new Identifier(_attribute) };

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => Name.CssFunction(_attribute);

        #endregion
    }
}
