namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;

    sealed class FontValueConverter : IValueConverter
    {
        public ICssValue Convert(StringSource source)
        {
            var style = default(FontStyle?);
            var variant = default(FontVariant?);
            var weight = default(FontWeight?);
            var stretch = default(FontStretch?);
            var size = default(Length?);
            var lineHeight = default(Length?);
            var fontFamilies = default(ICssValue[]);
            var pos = 0;
            var c = source.SkipSpacesAndComments();

            do
            {
                pos = source.Index;

                if (!style.HasValue)
                {
                    style = source.ParseConstant(Map.FontStyles);
                    source.SkipSpacesAndComments();
                }

                if (!variant.HasValue)
                {
                    variant = source.ParseConstant(Map.FontVariants);
                    source.SkipSpacesAndComments();
                }

                if (!weight.HasValue)
                {
                    weight = source.ParseConstant(Map.FontWeights);
                    source.SkipSpacesAndComments();
                }

                if (!stretch.HasValue)
                {
                    stretch = source.ParseConstant(Map.FontStretches);
                    source.SkipSpacesAndComments();
                }
            }
            while (pos != source.Index);
            
            size = source.ParseFontSize();

            if (size.HasValue)
            {
                c = source.SkipSpacesAndComments();

                if (c == Symbols.Solidus)
                {
                    source.SkipCurrentAndSpaces();
                    lineHeight = source.ParseLineHeight();
                    source.SkipSpacesAndComments();

                    if (!lineHeight.HasValue)
                    {
                        return null;
                    }
                }

                fontFamilies = source.ParseFontFamilies();

                if (fontFamilies != null)
                {
                    return new FontValue(style, variant, weight, stretch, size.Value, lineHeight, fontFamilies);
                }
            }

            return null;
        }

        sealed class FontValue : ICssValue
        {
            private readonly ICssValue[] _fontFamilies;
            private readonly Length? _lineHeight;
            private readonly Length _size;
            private readonly FontStretch? _stretch;
            private readonly FontStyle? _style;
            private readonly FontVariant? _variant;
            private readonly FontWeight? _weight;

            public FontValue(FontStyle? style, FontVariant? variant, FontWeight? weight, FontStretch? stretch, Length size, Length? lineHeight, ICssValue[] fontFamilies)
            {
                _style = style;
                _variant = variant;
                _weight = weight;
                _stretch = stretch;
                _size = size;
                _lineHeight = lineHeight;
                _fontFamilies = fontFamilies;
            }

            public String CssText
            {
                get
                {
                    var sb = StringBuilderPool.Obtain();

                    if (_style.HasValue)
                    {
                        if (sb.Length > 0) sb.Append(Symbols.Space);
                        sb.Append(_style.Value.ToString(Map.FontStyles));
                    }

                    if (_variant.HasValue)
                    {
                        if (sb.Length > 0) sb.Append(Symbols.Space);
                        sb.Append(_variant.Value.ToString(Map.FontVariants));
                    }

                    if (_weight.HasValue)
                    {
                        if (sb.Length > 0) sb.Append(Symbols.Space);
                        sb.Append(_weight.Value.ToString(Map.FontWeights));
                    }

                    if (_stretch.HasValue)
                    {
                        if (sb.Length > 0) sb.Append(Symbols.Space);
                        sb.Append(_stretch.Value.ToString(Map.FontStretches));
                    }

                    if (sb.Length > 0) sb.Append(Symbols.Space);
                    sb.Append(_size.ToString(Map.FontSizes));

                    if (_lineHeight.HasValue)
                    {
                        sb.Append(" / ");
                        sb.Append(_lineHeight.Value.ToString());
                    }

                    sb.Append(Symbols.Space);
                    sb.Append(_fontFamilies[0].CssText);

                    for (var i = 1; i < _fontFamilies.Length; i++)
                    {
                        sb.Append(", ").Append(_fontFamilies[i].CssText);
                    }

                    return sb.ToPool();
                }
            }
        }
    }
}
