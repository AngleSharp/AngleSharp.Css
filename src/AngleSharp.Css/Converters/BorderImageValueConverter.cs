namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;

    sealed class BorderImageValueConverter : IValueConverter
    {
        public ICssValue Convert(StringSource source)
        {
            var image = default(ICssValue);
            var slice = default(ICssValue);
            var widths = default(ICssValue);
            var outsets = default(ICssValue);
            var repeatX = default(ICssValue);
            var repeatY = default(ICssValue);
            var pos = 0;

            do
            {
                pos = source.Index;

                if (image == null)
                {
                    image = source.ParseImageSource();
                    source.SkipSpacesAndComments();
                }

                if (slice == null)
                {
                    slice = source.ParseBorderImageSlice();
                    var c = source.SkipSpacesAndComments();

                    if (slice != null && c == Symbols.Solidus)
                    {
                        source.SkipCurrentAndSpaces();
                        widths = source.ParsePeriodic<Length>(UnitParser.ParseBorderWidth);
                        c = source.SkipSpacesAndComments();

                        if (widths != null && c == Symbols.Solidus)
                        {
                            source.SkipCurrentAndSpaces();
                            outsets = source.ParsePeriodic<Length>(UnitParser.ParseDistance);

                            if (outsets == null)
                            {
                                return null;
                            }

                            source.SkipSpacesAndComments();
                        }
                    }
                }

                if (repeatX == null)
                {
                    repeatX = source.ParseConstant(Map.BorderRepeats);
                    source.SkipSpacesAndComments();
                }

                if (repeatY == null)
                {
                    repeatY = source.ParseConstant(Map.BorderRepeats);
                    source.SkipSpacesAndComments();
                }
            }
            while (pos != source.Index);

            return new BorderImage(image, slice, widths, outsets, CreateRepeat(repeatX, repeatY));
        }

        private static ICssValue CreateRepeat(ICssValue repeatX, ICssValue repeatY)
        {
            if (repeatX == null)
            {
                return null;
            }
            else if (repeatY == null)
            {
                return repeatX;
            }
            else
            {
                return new OrderedOptions(new[] { repeatX, repeatY });
            }
        }
    }
}
