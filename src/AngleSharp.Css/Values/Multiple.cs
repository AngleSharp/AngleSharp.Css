namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;

    public sealed class Multiple : ICssValue
    {
        private readonly ICssValue[] _items;

        public Multiple(ICssValue[] items)
        {
            _items = items;
        }

        public ICssValue[] Values
        {
            get { return _items; }
        }

        public String CssText
        {
            get { return ToString(); }
        }

        public override String ToString()
        {
            return _items.Join(", ");
        }
    }
}
