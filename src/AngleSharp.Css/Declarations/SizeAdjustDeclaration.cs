namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class SizeAdjustDeclaration
    {
        public static String Name = PropertyNames.SizeAdjust;

        public static IValueConverter Converter = SizeAdjustConverter;

        public static ICssValue InitialValue = InitialValues.SizeAdjustDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
