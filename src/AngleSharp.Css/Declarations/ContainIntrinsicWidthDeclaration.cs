namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ContainIntrinsicWidthDeclaration
    {
        public static String Name = PropertyNames.ContainIntrinsicWidth;

        public static IValueConverter Converter = ContainIntrinsicWidthConverter;

        public static ICssValue InitialValue = InitialValues.ContainIntrinsicWidthDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
