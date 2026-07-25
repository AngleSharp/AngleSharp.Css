namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class OutlineOffsetDeclaration
    {
        public static String Name = PropertyNames.OutlineOffset;

        public static IValueConverter Converter = OutlineOffsetConverter;

        public static ICssValue InitialValue = InitialValues.OutlineOffsetDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
