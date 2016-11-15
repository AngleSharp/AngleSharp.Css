﻿namespace AngleSharp.Css.Parser.Tokens
{
    using System;

    /// <summary>
    /// Represents a CSS unit token.
    /// </summary>
    sealed class CssUnitToken : CssNumberToken
    {
        #region Fields

        private readonly String _unit;

        #endregion

        #region ctor

        public CssUnitToken(CssTokenType type, String value, String dimension)
            : base(type, value)
        {
            _unit = dimension;
        }

        #endregion

        #region Properties

        public String Unit
        {
            get { return _unit; }
        }

        #endregion
    }
}
