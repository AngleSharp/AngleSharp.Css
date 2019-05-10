namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class JustifyContentDeclaration
    {
        public static String Name = PropertyNames.JustifyContent;

        public static IValueConverter Converter = Or(JustifyContentConverter, AssignInitial(InitialValues.JustifyContentDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
