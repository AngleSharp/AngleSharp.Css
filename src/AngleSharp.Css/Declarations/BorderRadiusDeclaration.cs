namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static ValueConverters;

    static class BorderRadiusDeclaration
    {
        public static String Name = PropertyNames.BorderRadius;

        public static IValueConverter Converter = new BorderRadiusAggregator();

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderTopLeftRadius,
            PropertyNames.BorderTopRightRadius,
            PropertyNames.BorderBottomLeftRadius,
            PropertyNames.BorderBottomRightRadius,
        };

        sealed class BorderRadiusValueConverter : IValueConverter
        {
            private readonly IValueConverter _converter = LengthOrPercentConverter.Periodic();

            public ICssValue Convert(StringSource source)
            {
                var start = source.Index;
                var horizontal = _converter.Convert(source) as Periodic<ICssValue>;
                var vertical = horizontal;

                if (source.Current == Symbols.Solidus)
                {
                    source.SkipCurrentAndSpaces();
                    vertical = _converter.Convert(source) as Periodic<ICssValue>;
                }

                return vertical != null ? new BorderRadius(horizontal, vertical) : null;
            }
        }

        sealed class BorderRadiusAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(new BorderRadiusValueConverter(), AssignInitial());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

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
}
