namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System.IO;

    sealed class UnorderedOptionsConverter : IValueConverter
    {
        private readonly IValueConverter[] _converters;

        public UnorderedOptionsConverter(params IValueConverter[] converters)
        {
            _converters = converters;
        }

        public ICssValue Convert(StringSource source)
        {
            var options = new ICssValue[_converters.Length];
            var i = 0;
            var failed = true;

            while (i < _converters.Length)
            {
                if (options[i] == null)
                {
                    var index = source.Index;
                    var option = _converters[i].Convert(source);

                    if (option != null)
                    {
                        source.SkipSpaces();
                        options[i] = option;
                        i = 0;
                        failed = false;
                        continue;
                    }

                    failed = true;
                    source.BackTo(index);
                }

                i++;
            }
            
            return failed ? null : new OptionsValue(options);
        }
        
        private sealed class OptionsValue : ICssValue
        {
            private readonly ICssValue[] _options;

            public OptionsValue(ICssValue[] options)
            {
                _options = options;
            }

            public string CssText
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
