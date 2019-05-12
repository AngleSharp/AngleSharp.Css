namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class GridAutoColumnsDeclaration
    {
        public static readonly String Name = PropertyNames.GridAutoColumns;

        public static readonly IValueConverter Converter = GridAutoConverter;

        public static readonly ICssValue InitialValue = InitialValues.GridAutoColumnsDecl;

        public static readonly PropertyFlags Flags = PropertyFlags.None;
    }
}
