namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static ValueConverters;

    static class GapDeclaration
    {
        public static readonly String Name = PropertyNames.Gap;

        public static readonly String[] Longhands = new[]
        {
            PropertyNames.ColumnGap,
            PropertyNames.RowGap,
        };

        public static readonly IValueConverter Converter = new GapAggregagtor();

        public static readonly PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        sealed class GapAggregagtor : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(WithOrder(GapConverter, GapConverter), AssignInitial());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Collect(IEnumerable<ICssProperty> properties)
            {
                var row = properties.Where(m => m.Name == RowGapDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var col = properties.Where(m => m.Name == ColumnGapDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

                if (row != null || col != null)
                {
                    return new CssTupleValue(new[] { row, col });
                }

                return null;
            }

            public IEnumerable<ICssProperty> Distribute(ICssValue value)
            {
                var list = value as CssTupleValue;

                if (list != null)
                {
                    return new[]
                    {
                        new CssProperty(RowGapDeclaration.Name, RowGapDeclaration.Converter, RowGapDeclaration.Flags, list.Items[0]),
                        new CssProperty(ColumnGapDeclaration.Name, ColumnGapDeclaration.Converter, ColumnGapDeclaration.Flags, list.Items[1]),
                    };
                }

                return null;
            }
        }
    }
}
