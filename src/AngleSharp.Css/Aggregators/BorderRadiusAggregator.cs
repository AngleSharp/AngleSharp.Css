namespace AngleSharp.Css.Aggregators
{
    using AngleSharp.Css.Declarations;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using System.Collections.Generic;
    using System.Linq;

    sealed class BorderRadiusAggregator : IValueAggregator
    {
        private static ICssValue Both(ICssValue horizontal, ICssValue vertical)
        {
            return new OrderedOptions(new[] { horizontal, vertical });
        }

        private static IEnumerable<ICssProperty> CreateLonghands(ICssValue topLeft, ICssValue topRight, ICssValue bottomRight, ICssValue bottomLeft)
        {
            return new[]
            {
                new CssProperty(BorderTopLeftRadiusDeclaration.Name, BorderTopLeftRadiusDeclaration.Converter, BorderTopLeftRadiusDeclaration.Flags, topLeft),
                new CssProperty(BorderTopRightRadiusDeclaration.Name, BorderTopRightRadiusDeclaration.Converter, BorderTopRightRadiusDeclaration.Flags, topRight),
                new CssProperty(BorderBottomRightRadiusDeclaration.Name, BorderBottomRightRadiusDeclaration.Converter, BorderBottomRightRadiusDeclaration.Flags, bottomRight),
                new CssProperty(BorderBottomLeftRadiusDeclaration.Name, BorderBottomLeftRadiusDeclaration.Converter, BorderBottomLeftRadiusDeclaration.Flags, bottomLeft),
            };
        }

        public ICssValue Collect(IEnumerable<ICssProperty> properties)
        {
            var topLeft = properties.Where(m => m.Name == BorderTopLeftRadiusDeclaration.Name).Select(m => m.RawValue).FirstOrDefault() as OrderedOptions;
            var topRight = properties.Where(m => m.Name == BorderTopRightRadiusDeclaration.Name).Select(m => m.RawValue).FirstOrDefault() as OrderedOptions;
            var bottomRight = properties.Where(m => m.Name == BorderBottomRightRadiusDeclaration.Name).Select(m => m.RawValue).FirstOrDefault() as OrderedOptions;
            var bottomLeft = properties.Where(m => m.Name == BorderBottomLeftRadiusDeclaration.Name).Select(m => m.RawValue).FirstOrDefault() as OrderedOptions;

            if (topLeft != null && topRight != null && bottomRight != null && bottomLeft != null)
            {
                var horizontal = new Periodic<ICssValue>(new[] { topLeft.Options[0], topRight.Options[0], bottomRight.Options[0], bottomLeft.Options[0] });
                var vertical = new Periodic<ICssValue>(new[] { topLeft.Options[1], topRight.Options[1], bottomRight.Options[1], bottomLeft.Options[1] });
                return new BorderRadius(horizontal, vertical);
            }

            return null;
        }

        public IEnumerable<ICssProperty> Distribute(ICssValue value)
        {
            var radius = value as BorderRadius;

            if (radius != null)
            {
                return CreateLonghands(Both(radius.Horizontal.Top, radius.Vertical.Top), Both(radius.Horizontal.Right, radius.Vertical.Right), Both(radius.Horizontal.Bottom, radius.Vertical.Bottom), Both(radius.Horizontal.Left, radius.Vertical.Left));
            }

            return null;
        }
    }
}