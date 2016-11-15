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
        private readonly Char _quote;

        #endregion

        #region ctor

        public CssStringToken(String data, Char quote, Boolean bad = false)
            : base(CssTokenType.String, data)
        {
            _bad = bad;
            _quote = quote;
        }

        #endregion

        #region Properties

        public Boolean IsBad
        {
            get { return _bad; }
        }

        public Char Quote
        {
            get { return _quote; }
        }

        #endregion
    }
}
