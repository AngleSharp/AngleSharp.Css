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

    static class BorderStyleDeclaration
    {
        public static String Name = PropertyNames.BorderStyle;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
        };

        public static IValueConverter Converter = new BorderStyleAggregator();

        public static PropertyFlags Flags = PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderLeftStyle,
            PropertyNames.BorderRightStyle,
            PropertyNames.BorderTopStyle,
            PropertyNames.BorderBottomStyle,
        };

        sealed class BorderStyleAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(LineStyleConverter.Periodic(), AssignInitial());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Collect(IEnumerable<ICssProperty> properties)
            {
                var top = properties.Where(m => m.Name == BorderTopStyleDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var right = properties.Where(m => m.Name == BorderRightStyleDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var bottom = properties.Where(m => m.Name == BorderBottomStyleDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var left = properties.Where(m => m.Name == BorderLeftStyleDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

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
                    new CssProperty(BorderTopStyleDeclaration.Name, BorderTopStyleDeclaration.Converter, BorderTopStyleDeclaration.Flags, periodic.Top),
                    new CssProperty(BorderRightStyleDeclaration.Name, BorderRightStyleDeclaration.Converter, BorderRightStyleDeclaration.Flags, periodic.Right),
                    new CssProperty(BorderBottomStyleDeclaration.Name, BorderBottomStyleDeclaration.Converter, BorderBottomStyleDeclaration.Flags, periodic.Bottom),
                    new CssProperty(BorderLeftStyleDeclaration.Name, BorderLeftStyleDeclaration.Converter, BorderLeftStyleDeclaration.Flags, periodic.Left),
                };
                }

                return null;
            }
        }
    }
}
