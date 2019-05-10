namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class ListStyleTypeDeclaration
    {
        public static String Name = PropertyNames.ListStyleType;

        public static String[] Shorthands = new[]
        {
            PropertyNames.ListStyle,
        };

        public static IValueConverter Converter = Or(ListStyleConverter, AssignInitial(InitialValues.ListStyleTypeDecl));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
