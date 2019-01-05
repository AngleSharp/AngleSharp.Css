namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Take a domain.
    /// </summary>
    sealed class DomainFunction : DocumentFunction
    {
        #region Fields

        private readonly String _subdomain;

        #endregion

        #region ctor

        public DomainFunction(String url)
            : base(FunctionNames.Domain, url)
        {
            _subdomain = "." + url;
        }

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
