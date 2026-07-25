namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TouchActionDeclaration
    {
        public static String Name = PropertyNames.TouchAction;

        public static IValueConverter Converter = TouchActionConverter;

        public static ICssValue InitialValue = InitialValues.TouchActionDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
