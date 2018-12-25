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

    static class BorderDeclaration
    {
        public static String Name = PropertyNames.Border;

        public static IValueConverter Converter = new BorderAggregator();

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderWidth,
            PropertyNames.BorderStyle,
            PropertyNames.BorderColor,
        };

        sealed class BorderValueConverter : IValueConverter
        {
            private static readonly IValueConverter converter = WithAny(
                LineWidthConverter.Option(),
                LineStyleConverter.Option(),
                CurrentColorConverter.Option());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }
        }

        sealed class BorderAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(new BorderValueConverter(), AssignInitial());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Collect(IEnumerable<ICssProperty> properties)
            {
                var width = properties.Where(m => m.Name == BorderWidthDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var style = properties.Where(m => m.Name == BorderStyleDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var color = properties.Where(m => m.Name == BorderColorDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

                if (width != null && style != null && color != null)
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
                    new CssProperty(BorderWidthDeclaration.Name, BorderWidthDeclaration.Converter, BorderWidthDeclaration.Flags, options.Options[0]),
                    new CssProperty(BorderStyleDeclaration.Name, BorderStyleDeclaration.Converter, BorderStyleDeclaration.Flags, options.Options[1]),
                    new CssProperty(BorderColorDeclaration.Name, BorderColorDeclaration.Converter, BorderColorDeclaration.Flags, options.Options[2]),
                };
                }

                return null;
            }
        }
    }
}
