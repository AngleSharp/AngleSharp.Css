namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ClearDeclaration
    {
        public static String Name = PropertyNames.Clear;

        public static IValueConverter Converter = ClearModeConverter;

        public static ICssValue InitialValue = InitialValues.ClearDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
