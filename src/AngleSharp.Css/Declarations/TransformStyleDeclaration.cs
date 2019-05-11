namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TransformStyleDeclaration
    {
        public static String Name = PropertyNames.TransformStyle;

        public static IValueConverter Converter = Toggle(CssKeywords.Flat, CssKeywords.Preserve3d);

        public static ICssValue InitialValue = InitialValues.TransformStyleDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
