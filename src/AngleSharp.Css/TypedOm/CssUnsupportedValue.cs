namespace AngleSharp.Css.TypedOm
{
    using System;

    /// <summary>
    /// A value without a more specific representation in the Typed OM, which is
    /// tied to the property it was obtained for.
    /// https://www.w3.org/TR/css-typed-om-1/#reify-as-a-cssstylevalue
    /// </summary>
    sealed class CssUnsupportedValue : CssStyleValue
    {
        private readonly String _property;
        private readonly String _text;

        public CssUnsupportedValue(String property, String text)
        {
            _property = property;
            _text = text;
        }

        /// <summary>
        /// Gets the name of the property the value belongs to.
        /// </summary>
        public String Property => _property;

        public override String ToString() => _text;
    }
}
