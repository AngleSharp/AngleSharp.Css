namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class OverscrollBehaviorInlineDeclaration
    {
        public static String Name = PropertyNames.OverscrollBehaviorInline;

        public static IValueConverter Converter = OverscrollBehaviorConverter;

        public static ICssValue InitialValue = InitialValues.OverscrollBehaviorInlineDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
