namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
    using System.IO;

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

        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write(CssText);
        }
    }
}
