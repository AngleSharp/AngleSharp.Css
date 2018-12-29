namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static ValueConverters;

    static class BorderTopDeclaration
    {
        public static String Name = PropertyNames.BorderTop;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
        };

        public static IValueConverter Converter = new BorderTopAggregator();

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderTopWidth,
            PropertyNames.BorderTopStyle,
            PropertyNames.BorderTopColor,
        };

        sealed class BorderTopAggregator : IValueAggregator, IValueConverter
        {
            public ICssValue Convert(StringSource source)
            {
                return BorderSideConverter.Convert(source);
            }

            public ICssValue Collect(IEnumerable<ICssProperty> properties)
            {
                var width = properties.Where(m => m.Name == BorderTopWidthDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var style = properties.Where(m => m.Name == BorderTopStyleDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var color = properties.Where(m => m.Name == BorderTopColorDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

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
                        new CssProperty(BorderTopWidthDeclaration.Name, BorderTopWidthDeclaration.Converter, BorderTopWidthDeclaration.Flags, options.Items[0]),
                        new CssProperty(BorderTopStyleDeclaration.Name, BorderTopStyleDeclaration.Converter, BorderTopStyleDeclaration.Flags, options.Items[1]),
                        new CssProperty(BorderTopColorDeclaration.Name, BorderTopColorDeclaration.Converter, BorderTopColorDeclaration.Flags, options.Items[2]),
                    };
                }

                return null;
            }
        }
    }
}
