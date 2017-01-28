namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    static class CompoundParser
    {
        public static Quotes ParseQuotes(this StringSource source)
        {
            var quotes = new List<Quote>();

            while (!source.IsDone)
            {
                var open = source.ParseString();
                source.SkipSpacesAndComments();
                var close = source.ParseString();
                source.SkipSpacesAndComments();

                if (open == null || close == null)
                {
                    return null;
                }

                quotes.Add(new Quote(open, close));
            }

            return new Quotes(quotes);
        }

        public static BorderImageSlice? ParseBorderImageSlice(this StringSource source)
        {
            var lengths = new Length[4];
            var filled = false;
            var completed = 0;
            var pos = 0;

            do
            {
                pos = source.Index;

                if (completed < 4)
                {
                    var length = source.ParsePercentOrNumber();

                    if (length.HasValue)
                    {
                        lengths[completed++] = length.Value;
                        source.SkipCurrentAndSpaces();
                    }
                }

                if (!filled)
                {
                    filled = source.IsIdentifier(CssKeywords.Fill);
                    source.SkipSpacesAndComments();
                }
            }
            while (source.Index != pos);

            if (completed > 0 || filled)
            {
                while (completed < 4)
                {
                    lengths[completed++] = Length.Auto;
                }

                return new BorderImageSlice(lengths[0], lengths[1], lengths[2], lengths[3], filled);
            }

            return null;
        }

        public static ImageRepeats? ParseBackgroundRepeat(this StringSource source)
        {
            if (source.IsIdentifier(CssKeywords.RepeatX))
            {
                return new ImageRepeats(BackgroundRepeat.Repeat, BackgroundRepeat.NoRepeat);
            }
            else if (source.IsIdentifier(CssKeywords.RepeatY))
            {
                return new ImageRepeats(BackgroundRepeat.NoRepeat, BackgroundRepeat.Repeat);
            }
            else
            {
                var repeatX = source.ParseConstant(Map.BackgroundRepeats);
                source.SkipSpacesAndComments();
                var repeatY = source.ParseConstant(Map.BackgroundRepeats);

                if (repeatY.HasValue)
                {
                    return new ImageRepeats(repeatX.Value, repeatY.Value);
                }
                else if (repeatX.HasValue)
                {
                    return new ImageRepeats(repeatX.Value, repeatX.Value);
                }
            }

            return null;
        }

        public static IImageSource ParseImageSource(this StringSource source)
        {
            var url = source.ParseUri();

            if (url == null)
            {
                return source.ParseGradient();
            }

            return url;
        }

        public static PeriodicValue<T> ParsePeriodic<T>(this StringSource source, Func<StringSource, T?> converter)
            where T : struct, ICssValue
        {
            var values = new List<T>(4);

            while (values.Count < 4)
            {
                var result = converter.Invoke(source);

                if (!result.HasValue)
                    break;

                values.Add(result.Value);
                source.SkipSpacesAndComments();
            }

            return values.Count > 0 ? new PeriodicValue<T>(values.ToArray()) : null;
        }

        public static BorderImage? ParseBorderImage(this StringSource source)
        {
            var image = default(IImageSource);
            var slice = default(BorderImageSlice?);
            var widths = default(PeriodicValue<Length>);
            var outsets = default(PeriodicValue<Length>);
            var repeatX = default(BorderRepeat?);
            var repeatY = default(BorderRepeat?);
            var pos = 0;

            do
            {
                pos = source.Index;

                if (image == null)
                {
                    image = source.ParseImageSource();
                    source.SkipSpacesAndComments();
                }

                if (!slice.HasValue)
                {
                    slice = source.ParseBorderImageSlice();
                    var c = source.SkipSpacesAndComments();

                    if (slice.HasValue && c == Symbols.Solidus)
                    {
                        source.SkipCurrentAndSpaces();
                        widths = source.ParsePeriodic<Length>(UnitParser.ParseBorderWidth);
                        c = source.SkipSpacesAndComments();

                        if (widths != null && c == Symbols.Solidus)
                        {
                            source.SkipCurrentAndSpaces();
                            outsets = source.ParsePeriodic<Length>(UnitParser.ParseDistance);

                            if (outsets == null)
                                return null;

                            source.SkipSpacesAndComments();
                        }
                    }
                }

                if (!repeatX.HasValue)
                {
                    repeatX = source.ParseConstant(Map.BorderRepeats);
                    source.SkipSpacesAndComments();
                }

                if (!repeatY.HasValue)
                {
                    repeatY = source.ParseConstant(Map.BorderRepeats);
                    source.SkipSpacesAndComments();
                }
            }
            while (pos != source.Index);

            return new BorderImage(image, slice, widths, outsets, repeatX, repeatY);
        }
    }
}
