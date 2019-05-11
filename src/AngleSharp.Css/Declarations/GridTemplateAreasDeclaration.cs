namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class GridTemplateAreasDeclaration
    {
        public static readonly String Name = PropertyNames.GridTemplateAreas;

        public static readonly String[] Shorthands = new[]
        {
            PropertyNames.GridTemplate,
        };

        public static readonly IValueConverter Converter = Or(None, StringConverter.Many());

        public static readonly ICssValue InitialValue = InitialValues.GridTemplateAreasDecl;

        public static readonly PropertyFlags Flags = PropertyFlags.None;
    }
}
