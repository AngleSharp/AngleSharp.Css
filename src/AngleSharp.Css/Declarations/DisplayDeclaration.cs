namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class DisplayDeclaration
    {
        public static String Name = PropertyNames.Display;

        public static IValueConverter Converter = Or(DisplayModeConverter, AssignInitial(InitialValues.DisplayDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
