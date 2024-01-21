namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;

    sealed class SeparatorConverter(IValueConverter converter, Char seperator) : IValueConverter
    {
        private readonly IValueConverter _converter = converter;
        private readonly Char _seperator = seperator;

        public ICssValue? Convert(StringSource source)
        {
            var value = _converter.Convert(source);

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
