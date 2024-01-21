namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Take a domain.
    /// </summary>
    sealed class DomainFunction(String url) : DocumentFunction(FunctionNames.Domain, url)
    {
        #region Fields

        private readonly String _subdomain = "." + url;

        #endregion
        
        #region ctor

        #endregion

        #region Methods

        public override Boolean Matches(Url url)
        {
            var domain = url.HostName;
            return domain.Isi(Data) || domain.EndsWith(_subdomain, StringComparison.OrdinalIgnoreCase);
        }

        #endregion
    }
}
