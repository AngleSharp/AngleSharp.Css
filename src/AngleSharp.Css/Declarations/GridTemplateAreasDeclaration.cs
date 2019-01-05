namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class GridTemplateAreasDeclaration
    {
        public static readonly String Name = PropertyNames.GridTemplateAreas;

        public static readonly String[] Shorthands = new[]
        {
            PropertyNames.GridTemplate,
        };

        public static readonly IValueConverter Converter = Or(None, StringConverter.Many(), AssignInitial());

        public static readonly PropertyFlags Flags = PropertyFlags.None;
    }
}
