namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class MaskTypeDeclaration
    {
        public static String Name = PropertyNames.MaskType;

        public static IValueConverter Converter = MaskTypeConverter;

        public static ICssValue InitialValue = InitialValues.MaskTypeDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
