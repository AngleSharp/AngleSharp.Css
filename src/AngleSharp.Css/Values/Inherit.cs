namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    sealed class Inherit : ICssValue
    {
        private Inherit()
        {
        }

        public static readonly Inherit Instance = new Inherit();

        public String CssText
        {
            get { return CssKeywords.Inherit; }
        }
    }
}
