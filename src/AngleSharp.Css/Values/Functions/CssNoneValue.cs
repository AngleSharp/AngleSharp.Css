namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    sealed class CssNoneValue : ICssImageValue
    {
        public String Name => CssKeywords.None;

        public ICssValue[] Arguments => Array.Empty<ICssValue>();

        public String CssText => CssKeywords.None;
    }
}
