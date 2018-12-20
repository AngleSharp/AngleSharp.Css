namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderLeftStyleDeclaration
    {
        public static String Name = PropertyNames.BorderLeftStyle;

        public static String Parent = PropertyNames.BorderStyle;

        public static IValueConverter Converter = Or(LineStyleConverter, AssignInitial(LineStyle.None));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
