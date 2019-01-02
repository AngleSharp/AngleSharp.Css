namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents an URL object.
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/uri
    /// </summary>
    public sealed class UrlReference : IImageSource, ICssFunctionValue
    {
        #region Fields

        private readonly String _path;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new URL reference for the given path.
        /// </summary>
        /// <param name="path">The path to reference.</param>
        public UrlReference(String path)
        {
            _path = path;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name
        {
            get { return FunctionNames.Url; }
        }

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments
        {
            get { return new[] { new StringValue(_path) }; }
        }

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get { return Name.CssFunction(_path.CssString()); }
        }

        /// <summary>
        /// Gets the referenced path.
        /// </summary>
        public String Path
        {
            get { return _path; }
        }

        #endregion
    }
}
