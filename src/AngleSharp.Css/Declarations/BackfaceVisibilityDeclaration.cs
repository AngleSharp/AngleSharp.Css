namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BackfaceVisibilityDeclaration
    {
        public static String Name = PropertyNames.BackfaceVisibility;

        public static IValueConverter Converter = BackfaceVisibilityConverter;

        public static ICssValue InitialValue = InitialValues.BackfaceVisibilityDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
