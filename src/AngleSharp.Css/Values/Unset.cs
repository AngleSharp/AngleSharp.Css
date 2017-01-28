namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
    using System.IO;

    struct Unset<T> : ICssValue
    {
        private readonly T _data;

        public Unset(T data)
        {
            _data = data;
        }

        public T Data
        {
            get { return _data; }
        }

        public String CssText
        {
            get { return CssKeywords.Unset; }
        }
    }
}
