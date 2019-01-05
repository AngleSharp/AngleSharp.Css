namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class OverflowXDeclaration
    {
        public static String Name = PropertyNames.OverflowX;

        public static IValueConverter Converter = Or(OverflowExtendedModeConverter, AssignInitial(OverflowMode.Visible));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
