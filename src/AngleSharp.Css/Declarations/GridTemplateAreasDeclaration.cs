namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class GridTemplateAreasDeclaration
    {
        public static readonly String Name = PropertyNames.GridTemplateAreas;

        public static readonly IValueConverter Converter = AssignInitial();

        public static readonly PropertyFlags Flags = PropertyFlags.None;
    }
}
