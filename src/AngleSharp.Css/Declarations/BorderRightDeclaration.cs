namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static ValueConverters;

    static class BorderRightDeclaration
    {
        public static String Name = PropertyNames.BorderRight;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
        };

        public static IValueConverter Converter = new BorderRightAggregator();

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderRightWidth,
            PropertyNames.BorderRightStyle,
            PropertyNames.BorderRightColor,
        };

        sealed class BorderRightAggregator : IValueAggregator, IValueConverter
        {
            public ICssValue Convert(StringSource source)
            {
                return BorderSideConverter.Convert(source);
            }

            public ICssValue Collect(IEnumerable<ICssProperty> properties)
            {
                var width = properties.Where(m => m.Name == BorderRightWidthDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var style = properties.Where(m => m.Name == BorderRightStyleDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var color = properties.Where(m => m.Name == BorderRightColorDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

                if (width != null || style != null || color != null)
                {
                    return new OrderedOptions(new[] { width, style, color });
                }

                return null;
            }

            public IEnumerable<ICssProperty> Distribute(ICssValue value)
            {
                var options = value as OrderedOptions;

                if (options != null)
                {
                    return new[]
                    {
                        new CssProperty(BorderRightWidthDeclaration.Name, BorderRightWidthDeclaration.Converter, BorderRightWidthDeclaration.Flags, options.Options[0]),
                        new CssProperty(BorderRightStyleDeclaration.Name, BorderRightStyleDeclaration.Converter, BorderRightStyleDeclaration.Flags, options.Options[1]),
                        new CssProperty(BorderRightColorDeclaration.Name, BorderRightColorDeclaration.Converter, BorderRightColorDeclaration.Flags, options.Options[2]),
                    };
                }

                return null;
            }
        }
    }
}
