namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class PositionVisibilityDeclaration
    {
        public static String Name = PropertyNames.PositionVisibility;

        public static IValueConverter Converter = PositionVisibilityConverter;

        public static ICssValue InitialValue = InitialValues.PositionVisibilityDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
