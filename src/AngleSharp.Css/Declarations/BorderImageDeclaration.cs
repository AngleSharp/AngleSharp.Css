namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class BorderImageDeclaration
    {
        public static String Name = PropertyNames.BorderImage;

        public static IValueConverter Converter = new BorderImageAggregator();

        public static PropertyFlags Flags = PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderImageOutset,
            PropertyNames.BorderImageRepeat,
            PropertyNames.BorderImageSlice,
            PropertyNames.BorderImageSource,
            PropertyNames.BorderImageWidth,
        };

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
                            widths = source.ParsePeriodic<ICssValue>(UnitParser.ParseBorderWidth);
                            c = source.SkipSpacesAndComments();

                            if (widths != null && c == Symbols.Solidus)
                            {
                                source.SkipCurrentAndSpaces();
                                outsets = source.ParsePeriodic<ICssValue>(UnitParser.ParseDistanceOrCalc);

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
                    return new CssTupleValue(new[] { repeatX, repeatY });
                }
            }
        }

        sealed class BorderImageAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(None, new BorderImageValueConverter(), AssignInitial());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Merge(ICssValue[] values)
            {
                var outset = values[0];
                var repeat = values[1];
                var slice = values[2];
                var source = values[3];
                var width = values[4];

                if (outset != null || repeat != null || slice != null || source != null || width != null)
                {
                    return new BorderImage(source, slice, width, outset, repeat);
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                var img = value as BorderImage?;

                if (img.HasValue)
                {
                    return new[]
                    {
                        img.Value.Outsets,
                        img.Value.Repeat,
                        img.Value.Slice,
                        img.Value.Image,
                        img.Value.Widths,
                    };
                }
                else
                {
                    var constant = value as Constant<Object>;

                    if (constant != null)
                    {
                        return new[]
                        {
                            null,
                            null,
                            null,
                            constant,
                            null,
                        };
                    }
                }

                return null;
            }
        }
    }
}
