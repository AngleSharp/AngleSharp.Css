namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Dom;
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Take a regular expression.
    /// </summary>
    sealed class RegexpFunction(String url) : DocumentFunction(FunctionNames.Regexp, url)
    {
        #region Fields

        private readonly Regex _regex = new Regex(url, RegexOptions.ECMAScript | RegexOptions.CultureInvariant);

        #endregion
        
        #region ctor

        #endregion

        #region Methods

        public override Boolean Matches(Url url)
        {
            return _regex.IsMatch(url.Href);
        }

        #endregion
    }
}
