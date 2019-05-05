namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS fit-content function call.
    /// </summary>
    class FitContent : ICssFunctionValue
    {
        #region Fields

        private readonly ICssValue _dim;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new fit-content function call.
        /// </summary>
        /// <param name="dim">The used dimension argument.</param>
        public FitContent(ICssValue dim)
        {
            _dim = dim;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the used dimension.
        /// </summary>
        public ICssValue Dimension => _dim;

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name => FunctionNames.FitContent;

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments => new[] { _dim };

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => Name.CssFunction(_dim.CssText);

        #endregion
    }
}
