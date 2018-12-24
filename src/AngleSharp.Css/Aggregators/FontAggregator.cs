namespace AngleSharp.Css.Aggregators
{
    using AngleSharp.Css.Declarations;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using System.Collections.Generic;
    using System.Linq;

    sealed class FontAggregator : IValueAggregator
    {
        public ICssValue Collect(IEnumerable<ICssProperty> properties)
        {
            var families = properties.Where(m => m.Name == FontFamilyDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
            var size = properties.Where(m => m.Name == FontSizeDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
            var stretch = properties.Where(m => m.Name == FontStretchDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
            var style = properties.Where(m => m.Name == FontStyleDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
            var variant = properties.Where(m => m.Name == FontVariantDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
            var weight = properties.Where(m => m.Name == FontWeightDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
            var height = properties.Where(m => m.Name == LineHeightDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

            if (families != null && size != null || families is Constant<SystemFont>)
            {
                return new FontInfo(style, variant, weight, stretch, size, height, families);
            }

            return null;
        }

        public IEnumerable<ICssProperty> Distribute(ICssValue value)
        {
            var font = value as FontInfo;

            if (font == null)
            {
                var systemFont = value as Constant<SystemFont>;

                if (systemFont == null)
                {
                    return null;
                }

                return CreateLonghands(systemFont, null, null, null, null, null, null);
            }

            return CreateLonghands(font.FontFamilies, font.Size, font.Stretch, font.Style, font.Variant, font.Weight, font.LineHeight);
        }

        private static IEnumerable<ICssProperty> CreateLonghands(ICssValue families, ICssValue size, ICssValue stretch, ICssValue style, ICssValue variant, ICssValue weight, ICssValue height)
        {
            return new[]
            {
                new CssProperty(FontFamilyDeclaration.Name, FontFamilyDeclaration.Converter, FontFamilyDeclaration.Flags, families),
                new CssProperty(FontSizeDeclaration.Name, FontSizeDeclaration.Converter, FontSizeDeclaration.Flags, size),
                new CssProperty(FontStretchDeclaration.Name, FontStretchDeclaration.Converter, FontStretchDeclaration.Flags, stretch),
                new CssProperty(FontStyleDeclaration.Name, FontStyleDeclaration.Converter, FontStyleDeclaration.Flags, style),
                new CssProperty(FontVariantDeclaration.Name, FontVariantDeclaration.Converter, FontVariantDeclaration.Flags, variant),
                new CssProperty(FontWeightDeclaration.Name, FontWeightDeclaration.Converter, FontWeightDeclaration.Flags, weight),
                new CssProperty(LineHeightDeclaration.Name, LineHeightDeclaration.Converter, LineHeightDeclaration.Flags, height),
            };
        }
    }
}