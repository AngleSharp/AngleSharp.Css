namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ContainDeclaration
    {
        public static String Name = PropertyNames.Contain;

        public static IValueConverter Converter = ContainConverter;

        public static ICssValue InitialValue = InitialValues.ContainDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
