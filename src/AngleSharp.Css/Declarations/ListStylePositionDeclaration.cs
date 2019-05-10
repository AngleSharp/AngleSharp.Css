namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class ListStylePositionDeclaration
    {
        public static String Name = PropertyNames.ListStylePosition;

        public static String[] Shorthands = new[]
        {
            PropertyNames.ListStyle,
        };

        public static IValueConverter Converter = Or(ListPositionConverter, AssignInitial(InitialValues.ListStylePositionDecl));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
