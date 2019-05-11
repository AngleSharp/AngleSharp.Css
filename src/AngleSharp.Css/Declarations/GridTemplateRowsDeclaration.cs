namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class GridTemplateRowsDeclaration
    {
        public static readonly String Name = PropertyNames.GridTemplateRows;

        public static readonly String[] Shorthands = new[]
        {
            PropertyNames.GridTemplate,
        };

        public static readonly IValueConverter Converter = GridTemplateConverter;

        public static readonly ICssValue InitialValue = InitialValues.GridTemplateRowsDecl;

        public static readonly PropertyFlags Flags = PropertyFlags.None;
    }
}
