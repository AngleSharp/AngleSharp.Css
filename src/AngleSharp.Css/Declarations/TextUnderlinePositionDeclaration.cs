namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TextUnderlinePositionDeclaration
    {
        public static String Name = PropertyNames.TextUnderlinePosition;

        public static IValueConverter Converter = Or(
            Assign(CssKeywords.Auto, CssKeywords.Auto),
            Assign("from-font", "from-font"),
            Assign(CssKeywords.Under, CssKeywords.Under),
            Assign("left", "left"),
            Assign("right", "right"));

        public static ICssValue InitialValue = InitialValues.TextUnderlinePositionDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}