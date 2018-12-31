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
                var child = width as CssChildValue ??
                    style as CssChildValue ??
                    color as CssChildValue;

                if (child != null)
                {
                    return child.Parent;
                }

                if (width != null && style != null && color != null)
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
                    return CreateProperties(options.Items[0], options.Items[1], options.Items[2]);
                }
                else if (value is VarReferences)
                {
                    var child = new CssChildValue(value);
                    return CreateProperties(child, child, child);
                }

                return null;
            }

            private static IEnumerable<ICssProperty> CreateProperties(ICssValue width, ICssValue style, ICssValue color)
            {
                return new[]
                {
                    new CssProperty(BorderWidthDeclaration.Name, BorderWidthDeclaration.Converter, BorderWidthDeclaration.Flags, width),
                    new CssProperty(BorderStyleDeclaration.Name, BorderStyleDeclaration.Converter, BorderStyleDeclaration.Flags, style),
                    new CssProperty(BorderColorDeclaration.Name, BorderColorDeclaration.Converter, BorderColorDeclaration.Flags, color),
                };
            }
        }
    }
}
