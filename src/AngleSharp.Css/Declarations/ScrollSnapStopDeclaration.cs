namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ScrollSnapStopDeclaration
    {
        public static String Name = PropertyNames.ScrollSnapStop;

        public static IValueConverter Converter = ScrollSnapStopConverter;

        public static ICssValue InitialValue = InitialValues.ScrollSnapStopDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
