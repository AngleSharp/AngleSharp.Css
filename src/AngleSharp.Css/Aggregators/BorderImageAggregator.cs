namespace AngleSharp.Css.Aggregators
{
    using AngleSharp.Css.Declarations;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using System.Collections.Generic;
    using System.Linq;

    sealed class BorderImageAggregator : IValueAggregator
    {
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

            return null;
        }
    }
}