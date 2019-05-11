namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class OverflowWrapDeclaration
    {
        public static String Name = PropertyNames.OverflowWrap;

        public static IValueConverter Converter = Or(OverflowWrapConverter, AssignInitial(InitialValues.OverflowWrapDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
