namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    public sealed class Constant<T> : ICssValue
    {
        private readonly String _key;
        private readonly T _data;

        public Constant(String key, T data)
        {
            _key = key;
            _data = data;
        }

        public String CssText
        {
            get { return ToString(); }
        }

        public override string ToString()
        {
            return _key;
        }
    }
}
