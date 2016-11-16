namespace AngleSharp.Css.Parser.Tokens
{
    using System;

    /// <summary>
    /// Represents a CSS URL token.
    /// </summary>
    sealed class CssUrlToken : CssToken
    {
        #region Fields

        private readonly Boolean _bad;

        #endregion

        #region ctor

        public CssUrlToken(String data, Boolean bad = false)
            : base(CssTokenType.Url, data)
        {
            _bad = bad;
        }

        #endregion

        #region Properties

        public Boolean IsBad
        {
            get { return _bad; }
        }

        #endregion
    }
}
