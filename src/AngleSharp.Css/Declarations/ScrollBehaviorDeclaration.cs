namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ScrollBehaviorDeclaration
    {
        public static String Name = PropertyNames.ScrollBehavior;

        public static IValueConverter Converter = ScrollBehaviorConverter;

        public static ICssValue InitialValue = InitialValues.ScrollBehaviorDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
