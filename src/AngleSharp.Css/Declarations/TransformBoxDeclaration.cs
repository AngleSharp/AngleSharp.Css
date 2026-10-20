namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TransformBoxDeclaration
    {
        public static String Name = PropertyNames.TransformBox;

        public static IValueConverter Converter = TransformBoxConverter;

        public static ICssValue InitialValue = InitialValues.TransformBoxDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
