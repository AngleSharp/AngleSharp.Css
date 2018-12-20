namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class CaptionSideDeclaration
    {
        public static String Name = PropertyNames.CaptionSide;

        public static IValueConverter Converter = Or(CaptionSideConverter, AssignInitial(true));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
