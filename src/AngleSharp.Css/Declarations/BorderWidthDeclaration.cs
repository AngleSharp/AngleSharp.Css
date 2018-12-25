namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static ValueConverters;

    static class BorderWidthDeclaration
    {
        public static String Name = PropertyNames.BorderWidth;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
        };

        public static IValueConverter Converter = new BorderWidthAggregator();

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderLeftWidth,
            PropertyNames.BorderRightWidth,
            PropertyNames.BorderTopWidth,
            PropertyNames.BorderBottomWidth,
        };

        sealed class BorderWidthAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(LineWidthConverter.Periodic(), AssignInitial());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Collect(IEnumerable<ICssProperty> properties)
            {
                var top = properties.Where(m => m.Name == BorderTopWidthDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var right = properties.Where(m => m.Name == BorderRightWidthDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var bottom = properties.Where(m => m.Name == BorderBottomWidthDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var left = properties.Where(m => m.Name == BorderLeftWidthDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

                if (top != null && right != null && bottom != null && left != null)
                {
                    return new Periodic<ICssValue>(new[] { top, right, bottom, left });
                }

                return null;
            }

            public IEnumerable<ICssProperty> Distribute(ICssValue value)
            {
                var periodic = value as Periodic<ICssValue>;

                if (periodic != null)
                {
                    return new[]
                    {
                        new CssProperty(BorderTopWidthDeclaration.Name, BorderTopWidthDeclaration.Converter, BorderTopWidthDeclaration.Flags, periodic.Top),
                        new CssProperty(BorderRightWidthDeclaration.Name, BorderRightWidthDeclaration.Converter, BorderRightWidthDeclaration.Flags, periodic.Right),
                        new CssProperty(BorderBottomWidthDeclaration.Name, BorderBottomWidthDeclaration.Converter, BorderBottomWidthDeclaration.Flags, periodic.Bottom),
                        new CssProperty(BorderLeftWidthDeclaration.Name, BorderLeftWidthDeclaration.Converter, BorderLeftWidthDeclaration.Flags, periodic.Left),
                    };
                }

                return null;
            }
        }
    }
}
