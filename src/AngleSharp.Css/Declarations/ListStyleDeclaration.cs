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

    static class ListStyleDeclaration
    {
        public static String Name = PropertyNames.ListStyle;

        public static IValueConverter Converter = new ListStyleAggregator();

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.ListStyleImage,
            PropertyNames.ListStylePosition,
            PropertyNames.ListStyleType,
        };

        sealed class ListStyleValueConverter : IValueConverter
        {
            private static readonly IValueConverter converter = WithAny(
                ListStyleConverter.Option(),
                ListPositionConverter.Option(),
                OptionalImageSourceConverter.Option());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }
        }

        sealed class ListStyleAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(new ListStyleValueConverter(), AssignInitial());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Collect(IEnumerable<ICssProperty> properties)
            {
                var type = properties.Where(m => m.Name == ListStyleTypeDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var position = properties.Where(m => m.Name == ListStylePositionDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var image = properties.Where(m => m.Name == ListStyleImageDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

                if (type != null || position != null || image != null)
                {
                    return new CssTupleValue(new[] { type, position, image });
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
                        new CssProperty(ListStyleTypeDeclaration.Name, ListStyleTypeDeclaration.Converter, ListStyleTypeDeclaration.Flags, options.Items[0]),
                        new CssProperty(ListStylePositionDeclaration.Name, ListStylePositionDeclaration.Converter, ListStylePositionDeclaration.Flags, options.Items[1]),
                        new CssProperty(ListStyleImageDeclaration.Name, ListStyleImageDeclaration.Converter, ListStyleImageDeclaration.Flags, options.Items[2]),
                    };
                }

                return null;
            }
        }
    }
}
