namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class WordWrapDeclaration
    {
        public static String Name = PropertyNames.WordWrap;

        public static IValueConverter Converter = Or(OverflowWrapConverter, AssignInitial(InitialValues.WordWrapDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
