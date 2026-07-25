namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class PrintColorAdjustDeclaration
    {
        public static String Name = PropertyNames.PrintColorAdjust;

        public static IValueConverter Converter = PrintColorAdjustConverter;

        public static ICssValue InitialValue = InitialValues.PrintColorAdjustDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
