namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ListStyleTypeDeclaration
    {
        public static String Name = PropertyNames.ListStyleType;

        public static String Parent = PropertyNames.ListStyle;

        public static IValueConverter Converter = Or(ListStyleConverter, AssignInitial(ListStyle.Disc));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
