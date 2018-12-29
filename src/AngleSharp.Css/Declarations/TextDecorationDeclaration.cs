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

    static class TextDecorationDeclaration
    {
        public static String Name = PropertyNames.TextDecoration;

        public static IValueConverter Converter = new TextDecorationAggregator();

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.TextDecorationColor,
            PropertyNames.TextDecorationLine,
            PropertyNames.TextDecorationStyle,
        };

        sealed class TextDecorationValueConverter : IValueConverter
        {
            private static readonly IValueConverter converter = WithAny(
                ColorConverter.Option(),
                TextDecorationStyleConverter.Option(),
                TextDecorationLinesConverter.Option());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }
        }

        sealed class TextDecorationAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(new TextDecorationValueConverter(), AssignInitial());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Collect(IEnumerable<ICssProperty> properties)
            {
                var color = properties.Where(m => m.Name == TextDecorationColorDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var style = properties.Where(m => m.Name == TextDecorationStyleDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var line = properties.Where(m => m.Name == TextDecorationLineDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

                if (color != null || style != null || line != null)
                {
                    return new CssTupleValue(new[] { color, style, line });
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
                        new CssProperty(TextDecorationColorDeclaration.Name, TextDecorationColorDeclaration.Converter, TextDecorationColorDeclaration.Flags, options.Items[0]),
                        new CssProperty(TextDecorationStyleDeclaration.Name, TextDecorationStyleDeclaration.Converter, TextDecorationStyleDeclaration.Flags, options.Items[1]),
                        new CssProperty(TextDecorationLineDeclaration.Name, TextDecorationLineDeclaration.Converter, TextDecorationLineDeclaration.Flags, options.Items[2]),
                    };
                }

                return null;
            }
        }
    }
}
