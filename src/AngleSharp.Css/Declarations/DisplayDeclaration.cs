namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class DisplayDeclaration
    {
        public static String Name = PropertyNames.Display;

        public static IValueConverter Converter = DisplayModeConverter;

        public static ICssValue InitialValue = InitialValues.DisplayDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
