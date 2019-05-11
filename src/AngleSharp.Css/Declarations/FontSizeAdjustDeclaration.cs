namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FontSizeAdjustDeclaration
    {
        public static String Name = PropertyNames.FontSizeAdjust;

        public static IValueConverter Converter = OptionalNumberConverter;

        public static ICssValue InitialValue = InitialValues.FontSizeAdjustDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Animatable;
    }
}
