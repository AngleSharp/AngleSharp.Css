namespace AngleSharp.Css.Parser.Tokens
{
    using System;

    /// <summary>
    /// Represents a CSS comment token.
    /// </summary>
    sealed class CssCommentToken(String data, Boolean bad) : CssToken(CssTokenType.Comment, data)
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
