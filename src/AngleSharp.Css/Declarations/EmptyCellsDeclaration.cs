namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class EmptyCellsDeclaration
    {
        public static String Name = PropertyNames.EmptyCells;

        public static IValueConverter Converter = EmptyCellsConverter;

        public static ICssValue InitialValue = InitialValues.EmptyCellsDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
