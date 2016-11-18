namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.IO;

    sealed class ContinuousValueConverter : IValueConverter
    {
        private readonly IValueConverter _converter;

        public ContinuousValueConverter(IValueConverter converter)
        {
            _converter = converter;
        }

        public ICssValue Convert(StringSource source)
        {
            var options = new List<ICssValue>();

            while (!source.IsDone)
            {
                var option = _converter.Convert(source);

                if (option == null)
                {
                    return null;
                }

                source.SkipSpacesAndComments();
                options.Add(option);
            }
            
            return new OptionsValue(options.ToArray());
        }

        private sealed class OptionsValue : ICssValue
        {
            private readonly ICssValue[] _options;

            public OptionsValue(ICssValue[] options)
            {
                _options = options;
            }

            public String CssText
            {
                get { return _options.Join(" "); }
            }

            public void ToCss(TextWriter writer, IStyleFormatter formatter)
            {
                writer.Write(CssText);
            }
        }
    }
}
