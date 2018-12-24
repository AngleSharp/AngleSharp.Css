namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;

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
                    return new FontInfo(style, variant, weight, stretch, size, lineHeight, new Multiple(fontFamilies));
                }
            }

            return null;
        }
    }
}
