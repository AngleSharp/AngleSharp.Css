namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;

    sealed class OrValueConverter : IValueConverter
    {
        private readonly IValueConverter[] _converters;

        public OrValueConverter(IValueConverter[] converters)
        {
            _converters = converters;
        }

        public ICssValue Convert(StringSource source)
        {
            var start = source.Index;

            for (var i = 0; i < _converters.Length; i++)
            {
                var result = _converters[i].Convert(source);

                if (result != null)
                {
                    return result;
                }

                source.BackTo(start);
            }


            return null;
        }
    }
}
