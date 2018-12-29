namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static ValueConverters;

    static class BorderLeftDeclaration
    {
        public static String Name = PropertyNames.BorderLeft;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
        };

        public static IValueConverter Converter = new BorderLeftAggregator();

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderLeftWidth,
            PropertyNames.BorderLeftStyle,
            PropertyNames.BorderLeftColor,
        };

        sealed class BorderLeftAggregator : IValueAggregator, IValueConverter
        {
            public ICssValue Convert(StringSource source)
            {
                return BorderSideConverter.Convert(source);
            }

            public ICssValue Collect(IEnumerable<ICssProperty> properties)
            {
                var width = properties.Where(m => m.Name == BorderLeftWidthDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var style = properties.Where(m => m.Name == BorderLeftStyleDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var color = properties.Where(m => m.Name == BorderLeftColorDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

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
                        new CssProperty(BorderLeftWidthDeclaration.Name, BorderLeftWidthDeclaration.Converter, BorderLeftWidthDeclaration.Flags, options.Items[0]),
                        new CssProperty(BorderLeftStyleDeclaration.Name, BorderLeftStyleDeclaration.Converter, BorderLeftStyleDeclaration.Flags, options.Items[1]),
                        new CssProperty(BorderLeftColorDeclaration.Name, BorderLeftColorDeclaration.Converter, BorderLeftColorDeclaration.Flags, options.Items[2]),
                    };
                }

                return null;
            }
        }
    }
}
