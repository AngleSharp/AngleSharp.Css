namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TextAlignDeclaration
    {
        public static String Name = PropertyNames.TextAlign;

        public static IValueConverter Converter = HorizontalAlignmentConverter;

        public static ICssValue InitialValue = InitialValues.TextAlignDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
