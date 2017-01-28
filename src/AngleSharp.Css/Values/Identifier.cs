namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    struct Identifier : ICssValue
    {
        private readonly String _text;

        public Identifier(String text)
        {
            _text = text;
        }

        public String CssText
        {
            get { return _text; }
        }

        public override String ToString()
        {
            return _text;
        }
    }
}
