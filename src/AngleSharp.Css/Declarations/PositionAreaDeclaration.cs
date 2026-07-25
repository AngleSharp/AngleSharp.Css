namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class PositionAreaDeclaration
    {
        public static String Name = PropertyNames.PositionArea;

        public static IValueConverter Converter = PositionAreaConverter;

        public static ICssValue InitialValue = InitialValues.PositionAreaDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
