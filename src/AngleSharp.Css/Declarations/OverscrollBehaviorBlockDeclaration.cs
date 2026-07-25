namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class OverscrollBehaviorBlockDeclaration
    {
        public static String Name = PropertyNames.OverscrollBehaviorBlock;

        public static IValueConverter Converter = OverscrollBehaviorConverter;

        public static ICssValue InitialValue = InitialValues.OverscrollBehaviorBlockDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
