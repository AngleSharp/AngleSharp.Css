namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS running function call.
    /// </summary>
    public sealed class CssRunningValue : ICssFunctionValue, IEquatable<CssRunningValue>
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
        public ICssValue[] Arguments => new ICssValue[] { new CssIdentifierValue(_ident) };

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => Name.CssFunction(_ident);

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssRunningValue other) => other is not null && _ident == other._ident;

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssRunningValue value && Equals(value);

        ICssValue ICssValue.Compute(ICssComputeContext context) => this;

        #endregion
    }
}
