namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class TransformStyleDeclaration
    {
        public static String Name = PropertyNames.TransformStyle;

        public static IValueConverter Converter = Or(Toggle(CssKeywords.Flat, CssKeywords.Preserve3d), AssignInitial(true));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
