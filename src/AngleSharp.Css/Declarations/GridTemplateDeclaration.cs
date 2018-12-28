namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class GridTemplateDeclaration
    {
        public static readonly String Name = PropertyNames.GridTemplate;

        public static readonly String[] Longhands = new[]
        {
            PropertyNames.GridTemplateAreas,
            PropertyNames.GridTemplateColumns,
            PropertyNames.GridTemplateRows,
        };

        public static readonly IValueConverter Converter = AssignInitial();

        public static readonly PropertyFlags Flags = PropertyFlags.Shorthand;
    }
}
