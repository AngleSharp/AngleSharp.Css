namespace AngleSharp.Css.Dom.Events
{
    using AngleSharp.Common;
    using AngleSharp.Css.Parser;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Events;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// The event that is published in case of an CSS parse error.
    /// </summary>
    /// <remarks>
    /// Creates a new CssParseErrorEvent event.
    /// </remarks>
    /// <param name="code">The provided error code.</param>
    /// <param name="position">The position in the source.</param>
    /// 
    public class CssErrorEvent(CssParseError code, TextPosition position) : Event(EventNames.Error)
    {
        #region Fields

        private readonly CssParseError _code = code;
        private readonly TextPosition _position = position;

        #endregion
        
        #region ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets the position of the error.
        /// </summary>
        public TextPosition Position => _position;

        /// <summary>
        /// Gets the provided error code.
        /// </summary>
        public Int32 Code => _code.GetCode();

        /// <summary>
        /// Gets the associated error message.
        /// </summary>
        public String Message => _code.GetMessage();

        #endregion
    }
}
