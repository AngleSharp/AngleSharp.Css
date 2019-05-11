namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class GridTemplateColumnsDeclaration
    {
        public static readonly String Name = PropertyNames.GridTemplateColumns;

        public static readonly String[] Shorthands = new[]
        {
            PropertyNames.GridTemplate,
        };

        public static readonly IValueConverter Converter = Or(GridTemplateConverter, AssignInitial(InitialValues.GridTemplateColumnsDecl));

        public static readonly PropertyFlags Flags = PropertyFlags.None;
    }
}
