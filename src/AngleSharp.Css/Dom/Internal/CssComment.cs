namespace AngleSharp.Css.Dom
{
    using System;
    using System.IO;

    /// <summary>
    /// Represents a comment in the CSS node tree.
    /// </summary>
    sealed class CssComment(String data) : ICssComment
    {
        #region Fields

        private readonly String _data = data;

        #endregion
        
        #region ctor

        #endregion

        #region Properties

        public String Data => _data;

        #endregion

        #region String Representation

        public void ToCss(TextWriter writer, IStyleFormatter formatter) => writer.Write(formatter.Comment(_data));

        #endregion
    }
}
