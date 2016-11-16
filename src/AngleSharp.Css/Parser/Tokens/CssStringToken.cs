namespace AngleSharp.Css.Parser.Tokens
{
    using System;

    /// <summary>
    /// Represents a CSS string token.
    /// </summary>
    sealed class CssStringToken : CssToken
    {
        #region Fields

        private readonly Boolean _bad;

        #endregion

        #region ctor

        public CssStringToken(String data, Boolean bad = false)
            : base(CssTokenType.String, data)
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
