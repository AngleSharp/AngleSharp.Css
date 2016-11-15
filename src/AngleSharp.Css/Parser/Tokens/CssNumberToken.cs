namespace AngleSharp.Css.Parser.Tokens
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents a CSS number token.
    /// </summary>
    class CssNumberToken : CssToken
    {
        #region Fields

        private static readonly Char[] floatIndicators = new[] { '.', 'e', 'E' };

        #endregion

        #region ctor
        
        public CssNumberToken(String number)
            : base(CssTokenType.Number, number)
        {
        }

        public CssNumberToken(CssTokenType type, String number)
            : base(type, number)
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if any floating indicators are positioned.
        /// </summary>
        public Boolean IsInteger
        {
            get { return Data.IndexOfAny(floatIndicators) == -1; }
        }

        /// <summary>
        /// Gets the contained integer value.
        /// </summary>
        public Int32 IntegerValue
        {
            get { return Int32.Parse(Data, CultureInfo.InvariantCulture); }
        }

        /// <summary>
        /// Gets the contained number.
        /// </summary>
        public Single Value
        {
            get { return Single.Parse(Data, CultureInfo.InvariantCulture); }
        }

        #endregion
    }
}
