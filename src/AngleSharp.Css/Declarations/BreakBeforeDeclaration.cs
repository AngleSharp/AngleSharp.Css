namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BreakBeforeDeclaration
    {
        public static String Name = PropertyNames.BreakBefore;

        public static IValueConverter Converter = Or(BreakModeConverter, AssignInitial(BreakMode.Auto));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
