namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static ValueConverters;

    static class ColumnsDeclaration
    {
        public static String Name = PropertyNames.Columns;

        public static IValueConverter Converter = new ColumnsAggregator();

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.ColumnWidth,
            PropertyNames.ColumnCount,
        };

        sealed class ColumnsValueConverter : IValueConverter
        {
            private static readonly IValueConverter converter = WithAny(
                AutoLengthConverter,
                OptionalIntegerConverter);

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }
        }

        sealed class ColumnsAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(new ColumnsValueConverter(), AssignInitial());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Collect(IEnumerable<ICssProperty> properties)
            {
                var width = properties.Where(m => m.Name == ColumnWidthDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var count = properties.Where(m => m.Name == ColumnCountDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

                if (width != null || count != null)
                {
                    return new CssTupleValue(new[] { width, count });
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
                        new CssProperty(ColumnWidthDeclaration.Name, ColumnWidthDeclaration.Converter, ColumnWidthDeclaration.Flags, options.Items[0]),
                        new CssProperty(ColumnCountDeclaration.Name, ColumnCountDeclaration.Converter, ColumnCountDeclaration.Flags, options.Items[1]),
                    };
                }

                return null;
            }
        }
    }
}
