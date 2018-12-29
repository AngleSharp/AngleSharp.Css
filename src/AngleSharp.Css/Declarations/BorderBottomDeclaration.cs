namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static ValueConverters;

    static class BorderBottomDeclaration
    {
        public static String Name = PropertyNames.BorderBottom;

        public static IValueConverter Converter = new BorderBottomAggregator();

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
        };

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderBottomWidth,
            PropertyNames.BorderBottomStyle,
            PropertyNames.BorderBottomColor,
        };

        sealed class BorderBottomAggregator : IValueAggregator, IValueConverter
        {
            public ICssValue Convert(StringSource source)
            {
                return BorderSideConverter.Convert(source);
            }

            public ICssValue Collect(IEnumerable<ICssProperty> properties)
            {
                var width = properties.Where(m => m.Name == BorderBottomWidthDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var style = properties.Where(m => m.Name == BorderBottomStyleDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var color = properties.Where(m => m.Name == BorderBottomColorDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

                if (width != null || style != null || color != null)
                {
                    return new CssTupleValue(new[] { width, style, color });
                }

                return null;
            }

            public IEnumerable<ICssProperty> Distribute(ICssValue value)
            {
                var options = value as CssTupleValue;

                if (options != null)
                {
                    return new[]
                    {
                        new CssProperty(BorderBottomWidthDeclaration.Name, BorderBottomWidthDeclaration.Converter, BorderBottomWidthDeclaration.Flags, options.Items[0]),
                        new CssProperty(BorderBottomStyleDeclaration.Name, BorderBottomStyleDeclaration.Converter, BorderBottomStyleDeclaration.Flags, options.Items[1]),
                        new CssProperty(BorderBottomColorDeclaration.Name, BorderBottomColorDeclaration.Converter, BorderBottomColorDeclaration.Flags, options.Items[2]),
                    };
                }

                return null;
            }
        }
    }
}
