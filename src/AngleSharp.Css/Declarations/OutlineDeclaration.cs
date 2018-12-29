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

    static class OutlineDeclaration
    {
        public static String Name = PropertyNames.Outline;

        public static IValueConverter Converter = new OutlineAggregator();

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.OutlineColor,
            PropertyNames.OutlineStyle,
            PropertyNames.OutlineWidth,
        };

        sealed class OutlineValueConverter : IValueConverter
        {
            private static readonly IValueConverter converter = WithAny(
                LineWidthConverter.Option(),
                LineStyleConverter.Option(),
                InvertedColorConverter.Option());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }
        }

        sealed class OutlineAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(new OutlineValueConverter(), AssignInitial());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Collect(IEnumerable<ICssProperty> properties)
            {
                var width = properties.Where(m => m.Name == OutlineWidthDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var style = properties.Where(m => m.Name == OutlineStyleDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var color = properties.Where(m => m.Name == OutlineColorDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

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
                        new CssProperty(OutlineWidthDeclaration.Name, OutlineWidthDeclaration.Converter, OutlineWidthDeclaration.Flags, options.Items[0]),
                        new CssProperty(OutlineStyleDeclaration.Name, OutlineStyleDeclaration.Converter, OutlineStyleDeclaration.Flags, options.Items[1]),
                        new CssProperty(OutlineColorDeclaration.Name, OutlineColorDeclaration.Converter, OutlineColorDeclaration.Flags, options.Items[2]),
                    };
                }

                return null;
            }
        }
    }
}
