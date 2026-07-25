namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class PositionAnchorDeclaration
    {
        public static String Name = PropertyNames.PositionAnchor;

        public static IValueConverter Converter = PositionAnchorConverter;

        public static ICssValue InitialValue = InitialValues.PositionAnchorDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
