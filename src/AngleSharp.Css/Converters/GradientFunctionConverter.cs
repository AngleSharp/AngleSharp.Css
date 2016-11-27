namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.IO;

    sealed class GradientFunctionConverter : IValueConverter
    {
        public ICssValue Convert(StringSource source)
        {
            var gradient = source.ParseGradient();

            if (gradient != null)
            {
                return new GradientValue(gradient);
            }

            return null;
        }

        private sealed class GradientValue : ICssValue
        {
            private readonly IGradient _gradient;

            public GradientValue(IGradient gradient)
            {
                _gradient = gradient;
            }

            public String CssText
            {
                get { return _gradient.ToString(); }
            }

            public void ToCss(TextWriter writer, IStyleFormatter formatter)
            {
                writer.Write(CssText);
            }
        }
    }
}
