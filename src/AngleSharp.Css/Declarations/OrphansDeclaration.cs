namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class OrphansDeclaration
    {
        public static String Name = PropertyNames.Orphans;

        public static IValueConverter Converter = Or(NaturalIntegerConverter, AssignInitial(InitialValues.OrphansDecl));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
