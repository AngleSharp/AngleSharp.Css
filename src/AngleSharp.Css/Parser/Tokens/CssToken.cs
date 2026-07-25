namespace AngleSharp.Css.Parser.Tokens
{
    using System;
    using AngleSharp.Text;

    /// <summary>
    /// The base class token for the CSS parser.
    /// </summary>
    struct CssToken
    {
        #region Fields

        private readonly CssTokenType _type;
        private readonly String _data;
        private TextPosition _position;

        #endregion

        #region ctor

        public CssToken(CssTokenType type, String data)
        {
            _type = type;
            _data = data;
        }

        #endregion

        #region Properties

        public CssTokenType Type => _type;

        public String Data => _data;

        public TextPosition Position
        {
            get => _position;
            set => _position = value;
        }

        #endregion
    }
}
