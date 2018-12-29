namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static ValueConverters;

    static class GridGapDeclaration
    {
        public static readonly String Name = PropertyNames.GridGap;

        public static readonly String[] Longhands = new[]
        {
            PropertyNames.GridColumnGap,
            PropertyNames.GridRowGap,
        };

        public static readonly IValueConverter Converter = new GridGapAggregagtor();

        public static readonly PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        sealed class GridGapAggregagtor : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(WithOrder(GapConverter, GapConverter), AssignInitial());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Collect(IEnumerable<ICssProperty> properties)
            {
                var row = properties.Where(m => m.Name == GridRowGapDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var col = properties.Where(m => m.Name == GridColumnGapDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

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
                        new CssProperty(GridRowGapDeclaration.Name, GridRowGapDeclaration.Converter, GridRowGapDeclaration.Flags, list.Items[0]),
                        new CssProperty(GridColumnGapDeclaration.Name, GridColumnGapDeclaration.Converter, GridColumnGapDeclaration.Flags, list.Items[1]),
                    };
                }

                return null;
            }
        }
    }
}
