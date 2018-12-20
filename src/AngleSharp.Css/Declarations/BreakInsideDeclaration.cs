namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BreakInsideDeclaration
    {
        public static String Name = PropertyNames.BreakInside;

        public static IValueConverter Converter = Or(BreakInsideModeConverter, AssignInitial(BreakMode.Auto));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
