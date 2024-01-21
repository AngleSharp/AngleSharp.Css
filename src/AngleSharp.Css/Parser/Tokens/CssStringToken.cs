namespace AngleSharp.Css.Parser.Tokens
{
    using System;

    /// <summary>
    /// Represents a CSS string token.
    /// </summary>
    sealed class CssStringToken(String data, Boolean bad = false) : CssToken(CssTokenType.String, data)
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
