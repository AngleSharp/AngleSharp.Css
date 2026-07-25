namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class OverscrollBehaviorYDeclaration
    {
        public static String Name = PropertyNames.OverscrollBehaviorY;

        public static String[] Shorthands = new[]
        {
            PropertyNames.OverscrollBehavior,
        };

        public static IValueConverter Converter = OverscrollBehaviorConverter;

        public static ICssValue InitialValue = InitialValues.OverscrollBehaviorYDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
