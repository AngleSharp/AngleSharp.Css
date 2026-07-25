namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ContainIntrinsicBlockSizeDeclaration
    {
        public static String Name = PropertyNames.ContainIntrinsicBlockSize;

        public static IValueConverter Converter = ContainIntrinsicBlockSizeConverter;

        public static ICssValue InitialValue = InitialValues.ContainIntrinsicBlockSizeDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
