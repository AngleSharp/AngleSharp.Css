namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class GridColumnDeclaration
    {
        public static readonly String Name = PropertyNames.GridColumn;

        public static readonly String[] Longhands = new[]
        {
            PropertyNames.GridColumnStart,
            PropertyNames.GridColumnEnd,
        };

        public static readonly IValueConverter Converter = AssignInitial();

        public static readonly PropertyFlags Flags = PropertyFlags.Shorthand;
    }
}
