namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class OverscrollBehaviorXDeclaration
    {
        public static String Name = PropertyNames.OverscrollBehaviorX;

        public static String[] Shorthands = new[]
        {
            PropertyNames.OverscrollBehavior,
        };

        public static IValueConverter Converter = OverscrollBehaviorConverter;

        public static ICssValue InitialValue = InitialValues.OverscrollBehaviorXDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
