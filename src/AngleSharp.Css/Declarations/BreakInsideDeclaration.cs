namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class BreakInsideDeclaration
    {
        public static String Name = PropertyNames.BreakInside;

        public static IValueConverter Converter = Or(BreakInsideModeConverter, AssignInitial(InitialValues.BreakInsideDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
