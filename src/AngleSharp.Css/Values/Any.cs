namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    public sealed class Any : ICssValue
    {
        private readonly String _text;

        public Any(String text)
        {
            _text = text;
        }

        public String CssText
        {
            get { return ToString(); }
        }

        public override String ToString()
        {
            return _text;
        }
    }
}
