namespace AngleSharp.Css.Parser.Tokens
{
    using System;
    using AngleSharp.Text;

    /// <summary>
    /// The base class token for the CSS parser.
    /// </summary>
    class CssToken(CssTokenType type, String data)
    {
        #region Fields

        private readonly CssTokenType _type = type;
        private readonly String _data = data;

        #endregion
        
        #region ctor

        #endregion

        #region Properties

        public CssTokenType Type => _type;

        public String Data => _data;

        public TextPosition Position
        {
            get;
            set;
        }

        #endregion
    }
}
