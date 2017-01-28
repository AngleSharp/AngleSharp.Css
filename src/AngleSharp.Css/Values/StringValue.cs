namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    sealed class StringValue : ICssValue
    {
        private readonly String _value;

        public StringValue(String value)
        {
            _value = value;
        }

        public String CssText
        {
            get { return _value.CssString(); }
        }
    }
}
