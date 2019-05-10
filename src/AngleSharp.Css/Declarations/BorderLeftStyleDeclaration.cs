namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class BorderLeftStyleDeclaration
    {
        public static String Name = PropertyNames.BorderLeftStyle;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderStyle,
            PropertyNames.BorderLeft,
        };

        public static IValueConverter Converter = Or(LineStyleConverter, AssignInitial(InitialValues.BorderLeftStyleDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
