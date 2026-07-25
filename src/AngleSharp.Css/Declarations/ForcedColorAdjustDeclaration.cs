namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ForcedColorAdjustDeclaration
    {
        public static String Name = PropertyNames.ForcedColorAdjust;

        public static IValueConverter Converter = ForcedColorAdjustConverter;

        public static ICssValue InitialValue = InitialValues.ForcedColorAdjustDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
