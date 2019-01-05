namespace AngleSharp.Css.Parser.Tokens
{
    using System;

    /// <summary>
    /// Represents a CSS comment token.
    /// </summary>
    sealed class CssCommentToken : CssToken
    {
        #region Fields

        private readonly Boolean _bad;

        #endregion

        #region ctor

        public CssCommentToken(String data, Boolean bad)
            : base(CssTokenType.Comment, data)
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
