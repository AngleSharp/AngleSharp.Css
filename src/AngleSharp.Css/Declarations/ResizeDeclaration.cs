namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ResizeDeclaration
    {
        public static String Name = PropertyNames.Resize;

        public static IValueConverter Converter = ResizeConverter;

        public static ICssValue InitialValue = InitialValues.ResizeDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
