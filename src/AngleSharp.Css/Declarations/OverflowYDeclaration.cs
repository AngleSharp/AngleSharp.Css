namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class OverflowYDeclaration
    {
        public static String Name = PropertyNames.OverflowY;

        public static IValueConverter Converter = Or(OverflowExtendedModeConverter, AssignInitial(InitialValues.OverflowDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
