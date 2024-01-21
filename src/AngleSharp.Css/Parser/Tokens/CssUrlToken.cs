namespace AngleSharp.Css.Parser.Tokens
{
    using System;

    /// <summary>
    /// Represents a CSS URL token.
    /// </summary>
    sealed class CssUrlToken(String data, Boolean bad = false) : CssToken(CssTokenType.Url, data)
    {
        #region Fields

        private readonly Boolean _bad = bad;

        #endregion
        
        #region ctor

        #endregion

        #region Properties

        public Boolean IsBad => _bad;

        #endregion
    }
}
