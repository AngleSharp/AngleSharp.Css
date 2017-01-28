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

        public String Path
        {
            get { return _path; }
        }

        #endregion

        #region Methods

        public override String ToString()
        {
            var fn = FunctionNames.Url;
            return fn.CssFunction(_path.CssString());
        }

        #endregion
    }
}
