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

    static class BorderColorDeclaration
    {
        public static String Name = PropertyNames.BorderColor;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
        };

        public static IValueConverter Converter = new BorderColorAggregator();

        public static PropertyFlags Flags = PropertyFlags.Hashless | PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderLeftColor,
            PropertyNames.BorderRightColor,
            PropertyNames.BorderTopColor,
            PropertyNames.BorderBottomColor,
        };

        sealed class BorderColorAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(CurrentColorConverter.Periodic(), AssignInitial());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Collect(IEnumerable<ICssProperty> properties)
            {
                var top = properties.Where(m => m.Name == BorderTopColorDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var right = properties.Where(m => m.Name == BorderRightColorDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var bottom = properties.Where(m => m.Name == BorderBottomColorDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var left = properties.Where(m => m.Name == BorderLeftColorDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

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
                        new CssProperty(BorderTopColorDeclaration.Name, BorderTopColorDeclaration.Converter, BorderTopColorDeclaration.Flags, periodic.Top),
                        new CssProperty(BorderRightColorDeclaration.Name, BorderRightColorDeclaration.Converter, BorderRightColorDeclaration.Flags, periodic.Right),
                        new CssProperty(BorderBottomColorDeclaration.Name, BorderBottomColorDeclaration.Converter, BorderBottomColorDeclaration.Flags, periodic.Bottom),
                        new CssProperty(BorderLeftColorDeclaration.Name, BorderLeftColorDeclaration.Converter, BorderLeftColorDeclaration.Flags, periodic.Left),
                    };
                }

                return null;
            }
        }
    }
}
