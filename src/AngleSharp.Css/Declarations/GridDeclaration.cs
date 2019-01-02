namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class GridDeclaration
    {
        public static readonly String Name = PropertyNames.Grid;

        public static readonly IValueConverter Converter = new GridAggregator();

        public static readonly String[] Longhands = new[]
        {
            PropertyNames.GridTemplateRows,
            PropertyNames.GridTemplateColumns,
            PropertyNames.GridTemplateAreas,
            PropertyNames.GridAutoRows,
            PropertyNames.GridAutoColumns,
            PropertyNames.GridAutoFlow,
            PropertyNames.GridRowGap,
            PropertyNames.GridColumnGap,
            PropertyNames.RowGap,
            PropertyNames.ColumnGap,
        };

        public static readonly PropertyFlags Flags = PropertyFlags.Shorthand;

        sealed class GridConverter : IValueConverter
        {
            public ICssValue Convert(StringSource source)
            {
                /*<'grid-template'> | <'grid-template-rows'> / [ auto-flow && dense? ] <'grid-auto-columns'>? | [ auto-flow && dense? ] <'grid-auto-rows'>? / <'grid-template-columns'>*/
                throw new NotImplementedException();
            }
        }

        sealed class GridAggregator : IValueConverter, IValueAggregator
        {
            private static readonly IValueConverter converter = Or(new GridConverter(), AssignInitial());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Merge(ICssValue[] values)
            {
                throw new NotImplementedException();
            }

            public ICssValue[] Split(ICssValue value)
            {
                throw new NotImplementedException();
            }
        }
    }
}
