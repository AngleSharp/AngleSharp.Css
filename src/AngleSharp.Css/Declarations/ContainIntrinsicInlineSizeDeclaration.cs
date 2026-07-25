namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ContainIntrinsicInlineSizeDeclaration
    {
        public static String Name = PropertyNames.ContainIntrinsicInlineSize;

        public static IValueConverter Converter = ContainIntrinsicInlineSizeConverter;

        public static ICssValue InitialValue = InitialValues.ContainIntrinsicInlineSizeDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
