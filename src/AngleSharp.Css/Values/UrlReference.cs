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
        private readonly String _path;

        public UrlReference(String path)
        {
            _path = path;
        }

        public String Path
        {
            get { return _path; }
        }

        public override String ToString()
        {
            var fn = FunctionNames.Url;
            return fn.CssFunction(_path.CssString());
        }
    }
}
