namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class OverflowXDeclaration
    {
        public static String Name = PropertyNames.OverflowX;

        public static IValueConverter Converter = Or(OverflowExtendedModeConverter, AssignInitial(InitialValues.OverflowDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
