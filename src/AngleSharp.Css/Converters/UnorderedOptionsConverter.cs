namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;

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

            while (i < _converters.Length)
            {
                if (options[i] == null)
                {
                    var option = _converters[i].Convert(source);

                    if (option != null)
                    {
                        source.SkipSpacesAndComments();
                        options[i] = option;
                        i = 0;
                        continue;
                    }
                }

                i++;
            }
            
            return new CssTupleValue(options);
        }
    }
}
