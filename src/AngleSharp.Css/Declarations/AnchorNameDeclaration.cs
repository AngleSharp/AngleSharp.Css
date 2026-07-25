namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class AnchorNameDeclaration
    {
        public static String Name = PropertyNames.AnchorName;

        public static IValueConverter Converter = AnchorNameConverter;

        public static ICssValue InitialValue = InitialValues.AnchorNameDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
