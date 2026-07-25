namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TabSizeDeclaration
    {
        public static String Name = PropertyNames.TabSize;

        public static IValueConverter Converter = TabSizeConverter;

        public static ICssValue InitialValue = InitialValues.TabSizeDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
