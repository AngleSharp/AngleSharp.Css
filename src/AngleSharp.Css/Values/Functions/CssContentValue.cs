namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS content function call.
    /// </summary>
    sealed class CssContentValue : ICssFunctionValue
    {
        #region Fields

        private readonly String _type;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new content function call.
        /// </summary>
        /// <param name="type">The used dimension argument.</param>
        public CssContentValue(String type)
        {
            _type = type;
        }

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
        public ICssValue[] Arguments => new ICssValue[] { new Identifier(_type) };

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => Name.CssFunction(_type);

        #endregion
    }
}
