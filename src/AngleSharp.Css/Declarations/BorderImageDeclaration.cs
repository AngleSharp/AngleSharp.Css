namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        sealed class BorderImageAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(None, new BorderImageValueConverter(), AssignInitial());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Collect(IEnumerable<ICssProperty> properties)
            {
                var outset = properties.Where(m => m.Name == BorderImageOutsetDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var repeat = properties.Where(m => m.Name == BorderImageRepeatDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var slice = properties.Where(m => m.Name == BorderImageSliceDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var source = properties.Where(m => m.Name == BorderImageSourceDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var width = properties.Where(m => m.Name == BorderImageWidthDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

                if (outset != null || repeat != null || slice != null || source != null || width != null)
                {
                    return new BorderImage(source, slice, width, outset, repeat);
                }

                return null;
            }

            public IEnumerable<ICssProperty> Distribute(ICssValue value)
            {
                var img = value as BorderImage?;

                if (img.HasValue)
                {
                    return new[]
                    {
                        new CssProperty(BorderImageOutsetDeclaration.Name, BorderImageOutsetDeclaration.Converter, BorderImageOutsetDeclaration.Flags, img.Value.Outsets),
                        new CssProperty(BorderImageRepeatDeclaration.Name, BorderImageRepeatDeclaration.Converter, BorderImageRepeatDeclaration.Flags, img.Value.Repeat),
                        new CssProperty(BorderImageSliceDeclaration.Name, BorderImageSliceDeclaration.Converter, BorderImageSliceDeclaration.Flags, img.Value.Slice),
                        new CssProperty(BorderImageSourceDeclaration.Name, BorderImageSourceDeclaration.Converter, BorderImageSourceDeclaration.Flags, img.Value.Image),
                        new CssProperty(BorderImageWidthDeclaration.Name, BorderImageWidthDeclaration.Converter, BorderImageWidthDeclaration.Flags, img.Value.Widths),
                    };
                }
                else
                {
                    var constant = value as Constant<Object>;

                    if (constant != null)
                    {
                        return new[]
                        {
                            new CssProperty(BorderImageOutsetDeclaration.Name, BorderImageOutsetDeclaration.Converter, BorderImageOutsetDeclaration.Flags, null),
                            new CssProperty(BorderImageRepeatDeclaration.Name, BorderImageRepeatDeclaration.Converter, BorderImageRepeatDeclaration.Flags, null),
                            new CssProperty(BorderImageSliceDeclaration.Name, BorderImageSliceDeclaration.Converter, BorderImageSliceDeclaration.Flags, null),
                            new CssProperty(BorderImageSourceDeclaration.Name, BorderImageSourceDeclaration.Converter, BorderImageSourceDeclaration.Flags, constant),
                            new CssProperty(BorderImageWidthDeclaration.Name, BorderImageWidthDeclaration.Converter, BorderImageWidthDeclaration.Flags, null)
                        };
                    }
                }

                return null;
            }
        }
    }
}
