namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ContainIntrinsicHeightDeclaration
    {
        public static String Name = PropertyNames.ContainIntrinsicHeight;

        public static IValueConverter Converter = ContainIntrinsicHeightConverter;

        public static ICssValue InitialValue = InitialValues.ContainIntrinsicHeightDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
