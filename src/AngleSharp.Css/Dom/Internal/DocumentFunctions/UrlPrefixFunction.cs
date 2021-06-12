﻿namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Take an url prefix.
    /// </summary>
    sealed class UrlPrefixFunction : DocumentFunction
    {
        #region ctor

        public UrlPrefixFunction(String url)
            : base(FunctionNames.UrlPrefix, url)
        {
        }

        #endregion

        #region Methods

        public override Boolean Matches(Url url)
        {
            return url.Href.StartsWith(Data, StringComparison.OrdinalIgnoreCase);
        }

        #endregion
    }
}
