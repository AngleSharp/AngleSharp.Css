﻿namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Dom;
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Take a regular expression.
    /// </summary>
    sealed class RegexpFunction : DocumentFunction
    {
        #region Fields

        private readonly Regex _regex;

        #endregion

        #region ctor

        public RegexpFunction(String url)
            : base(FunctionNames.Regexp, url)
        {
            _regex = new Regex(url, RegexOptions.ECMAScript | RegexOptions.CultureInvariant);
        }

        #endregion

        #region Methods

        public override Boolean Matches(Url url)
        {
            return _regex.IsMatch(url.Href);
        }

        #endregion
    }
}
