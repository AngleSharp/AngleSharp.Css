namespace AngleSharp.Css.Dom
{
    using System;
    using System.IO;

    class BaseValue : ICssValue
    {
        private readonly String _text;

        public BaseValue(String text)
        {
            _text = text;
        }

        public String CssText
        {
            get { return _text; }
        }

        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write(_text);
        }
    }
}
