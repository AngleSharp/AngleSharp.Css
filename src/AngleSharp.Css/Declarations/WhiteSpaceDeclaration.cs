namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class WhiteSpaceDeclaration
    {
        public static String Name = PropertyNames.WhiteSpace;

        public static IValueConverter Converter = Or(WhitespaceConverter, AssignInitial(InitialValues.WhiteSpaceDecl));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
