namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class GridDeclaration
    {
        public static readonly String Name = PropertyNames.Grid;

        public static readonly IValueConverter Converter = AssignInitial();

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
    }
}
