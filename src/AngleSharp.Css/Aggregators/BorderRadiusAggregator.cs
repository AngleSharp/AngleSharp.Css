namespace AngleSharp.Css.Aggregators
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Declarations;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    sealed class BorderRadiusAggregator : IValueAggregator
    {
        private static String Left(String parts)
        {
            var idx = parts.IndexOf(' ');
            return idx != -1 ? parts.Substring(0, idx) : parts;
        }

        private static String Right(String parts)
        {
            var idx = parts.IndexOf(' ');
            return idx != -1 ? parts.Substring(idx + 1) : String.Empty;
        }

        private static ICssValue Both(ICssValue horizontal, ICssValue vertical)
        {
            return new OrderedOptions(new[] { horizontal, vertical });
        }

        private static IEnumerable<ICssProperty> CreateLonghands(ICssValue topLeft, ICssValue topRight, ICssValue bottomRight, ICssValue bottomLeft)
        {
            return new[]
            {
                new CssProperty(PropertyNames.BorderTopLeftRadius, BorderTopLeftRadiusDeclaration.Converter, BorderTopLeftRadiusDeclaration.Flags, topLeft),
                new CssProperty(PropertyNames.BorderTopRightRadius, BorderTopRightRadiusDeclaration.Converter, BorderTopRightRadiusDeclaration.Flags, topRight),
                new CssProperty(PropertyNames.BorderBottomRightRadius, BorderBottomRightRadiusDeclaration.Converter, BorderBottomRightRadiusDeclaration.Flags, bottomRight),
                new CssProperty(PropertyNames.BorderBottomLeftRadius, BorderBottomLeftRadiusDeclaration.Converter, BorderBottomLeftRadiusDeclaration.Flags, bottomLeft),
            };
        }

        public ICssValue Collect(IEnumerable<ICssProperty> properties)
        {
            var topLeft = properties.Where(m => m.Name == PropertyNames.BorderTopLeftRadius).Select(m => m.Value).FirstOrDefault();
            var topRight = properties.Where(m => m.Name == PropertyNames.BorderTopRightRadius).Select(m => m.Value).FirstOrDefault();
            var bottomRight = properties.Where(m => m.Name == PropertyNames.BorderBottomRightRadius).Select(m => m.Value).FirstOrDefault();
            var bottomLeft = properties.Where(m => m.Name == PropertyNames.BorderBottomLeftRadius).Select(m => m.Value).FirstOrDefault();
            var valueLeft = $"{Left(topLeft)} {Left(topRight)} {Left(bottomRight)} {Left(bottomLeft)}".Trim();
            var valueRight = $"{Right(topLeft)} {Right(topRight)} {Right(bottomRight)} {Right(bottomLeft)}".Trim();
            var converter = new BorderRadiusValueConverter();
            return converter.Convert(valueRight.Length > 0 ? $"{valueLeft} / {valueRight}" : valueLeft);
        }

        public IEnumerable<ICssProperty> Distribute(ICssValue value)
        {
            var radius = value as BorderRadius;

            if (radius != null)
            {
                return CreateLonghands(Both(radius.Horizontal.Top, radius.Vertical.Top), Both(radius.Horizontal.Right, radius.Vertical.Right), Both(radius.Horizontal.Bottom, radius.Vertical.Bottom), Both(radius.Horizontal.Left, radius.Vertical.Left));
            }

            return CreateLonghands(null, null, null, null);
        }
    }
}