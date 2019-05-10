namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class BreakBeforeDeclaration
    {
        public static String Name = PropertyNames.BreakBefore;

        public static IValueConverter Converter = Or(BreakModeConverter, AssignInitial(InitialValues.BreakBeforeDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
