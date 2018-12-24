namespace AngleSharp.Css.Values
{
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents an URL object.
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/uri
    /// </summary>
    public sealed class UrlReference : IImageSource
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
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get { return ToString(); }
        }

        /// <summary>
        /// Gets the referenced path.
        /// </summary>
        public String Path
        {
            get { return _path; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Serializes to a string.
        /// </summary>
        public override String ToString()
        {
            var fn = FunctionNames.Url;
            return fn.CssFunction(_path.CssString());
        }

        #endregion
    }
}
