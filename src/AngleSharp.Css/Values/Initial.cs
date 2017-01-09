namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
    using System.IO;

    struct Initial<T> : ICssValue
    {
        private readonly T _data;

        public Initial(T data)
        {
            _data = data;
        }

        public T Data
        {
            get { return _data; }
        }

        public String CssText
        {
            get { return CssKeywords.Initial; }
        }

        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write(CssText);
        }
    }
}
