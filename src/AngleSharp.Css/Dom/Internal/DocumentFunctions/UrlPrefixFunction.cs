namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Take an url prefix.
    /// </summary>
    sealed class UrlPrefixFunction(String url) : DocumentFunction(FunctionNames.UrlPrefix, url)
    {

        #region ctor

        #endregion

        #region Methods

        public override Boolean Matches(Url url)
        {
            return url.Href.StartsWith(Data, StringComparison.OrdinalIgnoreCase);
        }

        #endregion
    }
}
