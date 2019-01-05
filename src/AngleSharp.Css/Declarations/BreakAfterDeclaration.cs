namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BreakAfterDeclaration
    {
        public static String Name = PropertyNames.BreakAfter;

        public static IValueConverter Converter = Or(BreakModeConverter, AssignInitial(BreakMode.Auto));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
