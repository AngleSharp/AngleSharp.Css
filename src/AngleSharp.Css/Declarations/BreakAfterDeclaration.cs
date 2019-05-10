namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class BreakAfterDeclaration
    {
        public static String Name = PropertyNames.BreakAfter;

        public static IValueConverter Converter = Or(BreakModeConverter, AssignInitial(InitialValues.BreakAfterDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
