namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class FontDeclaration
    {
        public static String Name = PropertyNames.Font;

        public static IValueConverter Converter = new FontAggregator();

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.FontFamily,
            PropertyNames.FontSize,
            PropertyNames.FontVariant,
            PropertyNames.FontWeight,
            PropertyNames.FontStretch,
            PropertyNames.FontStyle,
            PropertyNames.LineHeight,
        };

        sealed class FontValueConverter : IValueConverter
        {
            public ICssValue Convert(StringSource source)
            {
                var style = default(ICssValue);
                var variant = default(ICssValue);
                var weight = default(ICssValue);
                var stretch = default(ICssValue);
                var size = default(ICssValue);
                var lineHeight = default(ICssValue);
                var fontFamilies = default(ICssValue[]);
                var pos = 0;
                var c = source.SkipSpacesAndComments();

                do
                {
                    pos = source.Index;

                    if (style == null)
                    {
                        style = source.ParseConstant(Map.FontStyles);
                        source.SkipSpacesAndComments();
                    }

                    if (variant == null)
                    {
                        variant = source.ParseConstant(Map.FontVariants);
                        source.SkipSpacesAndComments();
                    }

                    if (weight == null)
                    {
                        weight = source.ParseConstant(Map.FontWeights);
                        source.SkipSpacesAndComments();
                    }

                    if (stretch == null)
                    {
                        stretch = source.ParseConstant(Map.FontStretches);
                        source.SkipSpacesAndComments();
                    }
                }
                while (pos != source.Index);

                size = source.ParseFontSize();

                if (size != null)
                {
                    c = source.SkipSpacesAndComments();

                    if (c == Symbols.Solidus)
                    {
                        source.SkipCurrentAndSpaces();
                        lineHeight = source.ParseLineHeight();
                        source.SkipSpacesAndComments();

                        if (lineHeight == null)
                        {
                            return null;
                        }
                    }

                    fontFamilies = source.ParseFontFamilies();

                    if (fontFamilies != null)
                    {
                        return new FontInfo(style, variant, weight, stretch, size, lineHeight, new CssListValue(fontFamilies));
                    }
                }

                return null;
            }
        }

        sealed class FontAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(new FontValueConverter(), SystemFontConverter, AssignInitial());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Merge(ICssValue[] values)
            {
                var families = values[0];
                var size = values[1];
                var variant = values[2];
                var weight = values[3];
                var stretch = values[4];
                var style = values[5];
                var height = values[6];

                if (families != null && size != null || families is Constant<SystemFont>)
                {
                    return new FontInfo(style, variant, weight, stretch, size, height, families);
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                var font = value as FontInfo;

                if (font == null)
                {
                    var systemFont = value as Constant<SystemFont>;

                    if (systemFont == null)
                    {
                        return null;
                    }

                    return new[] { systemFont, null, null, null, null, null, null };
                }

                return new[] { font.FontFamilies, font.Size, font.Variant, font.Weight, font.Stretch, font.Style, font.LineHeight };
            }
        }
    }
}
