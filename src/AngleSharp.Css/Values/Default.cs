namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    public sealed class Default<T> : ICssValue
    {
        private readonly T _option;

        public Default(T option)
        {
            _option = option;
        }

        public String CssText
        {
            get { return ToString(); }
        }

        public T Option
        {
            get { return _option; }
        }

        public override String ToString()
        {
            return String.Empty;
        }
    }
}
