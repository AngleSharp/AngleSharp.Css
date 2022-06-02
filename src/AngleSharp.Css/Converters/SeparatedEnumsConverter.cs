namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;

    sealed class SeparatedEnumsConverter : IValueConverter
    {
        private readonly IValueConverter[] _converters;
        private readonly Char _seperator;

        public SeparatedEnumsConverter(IValueConverter[] converters, Char seperator)
        {
            _converters = converters;
            _seperator = seperator;
        }
        public ICssValue Convert(StringSource source)
        {
            var value = _converters[0].Convert(source);

            if (value != null)
            {
                var c = source.SkipSpacesAndComments();

                if (!source.IsDone)
                {
                    if (c == _seperator)
                    {
                        source.SkipCurrentAndSpaces();
                    }
                    else
                    {
                        value = null;
                    }
                }
            }

            return value;
        }
    }
}
