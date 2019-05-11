namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BoxDecorationBreakDeclaration
    {
        public static String Name = PropertyNames.BoxDecorationBreak;

        public static IValueConverter Converter = BoxDecorationConverter;

        public static ICssValue InitialValue = InitialValues.BoxDecorationBreakDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
