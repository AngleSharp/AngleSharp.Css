namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents an URL object.
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/uri
    /// </summary>
    sealed class CssUrlValue : ICssImageValue, ICssFunctionValue
    {
        #region Fields

        private readonly String _path;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new URL reference for the given path.
        /// </summary>
        /// <param name="path">The path to reference.</param>
        public CssUrlValue(String path)
        {
            _path = path;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name => FunctionNames.Url;

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments => new ICssValue[] { new Label(_path) };

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => Name.CssFunction(_path.CssString());

        /// <summary>
        /// Gets the referenced path.
        /// </summary>
        public String Path => _path;

        #endregion
    }
}
