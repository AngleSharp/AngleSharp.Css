namespace AngleSharp.Css.Values
{
    using AngleSharp.Text;

    sealed class ExternalImage : IImageSource
    {
        private readonly Url _url;

        public ExternalImage(Url url)
        {
            _url = url;
        }

        public override string ToString()
        {
            return FunctionNames.Url.CssFunction(_url.Href.CssString());
        }
    }
}
