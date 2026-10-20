namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TextOrientationDeclaration
    {
        public static String Name = PropertyNames.TextOrientation;

        public static IValueConverter Converter = Or(
            Assign(CssKeywords.Mixed, CssKeywords.Mixed),
            Assign("upright", "upright"),
            Assign("sideways", "sideways"));

        public static ICssValue InitialValue = InitialValues.TextOrientationDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
