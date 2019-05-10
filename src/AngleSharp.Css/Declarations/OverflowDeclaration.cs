namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class OverflowDeclaration
    {
        public static String Name = PropertyNames.Overflow;

        public static IValueConverter Converter = Or(OverflowModeConverter, AssignInitial(InitialValues.OverflowDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
