namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ContainIntrinsicSizeDeclaration
    {
        public static String Name = PropertyNames.ContainIntrinsicSize;

        public static IValueConverter Converter = ContainIntrinsicSizeConverter;

        public static ICssValue InitialValue = InitialValues.ContainIntrinsicSizeDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
