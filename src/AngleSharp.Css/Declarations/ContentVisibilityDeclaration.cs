namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ContentVisibilityDeclaration
    {
        public static String Name = PropertyNames.ContentVisibility;

        public static IValueConverter Converter = VisibilityConverter;

        public static ICssValue InitialValue = InitialValues.ContentVisibilityDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
