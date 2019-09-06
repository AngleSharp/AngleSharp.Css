namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS running function call.
    /// </summary>
    sealed class CssRunningValue : ICssFunctionValue
    {
        #region Fields

        private readonly String _ident;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new running function call.
        /// </summary>
        /// <param name="ident">The used identifier argument.</param>
        public CssRunningValue(String ident)
        {
            _ident = ident;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the used identifier.
        /// </summary>
        public String Identifier => _ident;

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name => FunctionNames.Running;

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments => new ICssValue[] { new Identifier(_ident) };

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => Name.CssFunction(_ident);

        #endregion
    }
}
