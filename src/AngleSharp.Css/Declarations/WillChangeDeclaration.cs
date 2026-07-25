namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class WillChangeDeclaration
    {
        public static String Name = PropertyNames.WillChange;

        public static IValueConverter Converter = WillChangeConverter;

        public static ICssValue InitialValue = InitialValues.WillChangeDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
