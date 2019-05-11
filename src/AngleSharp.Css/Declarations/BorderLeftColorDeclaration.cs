namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class BorderLeftColorDeclaration
    {
        public static String Name = PropertyNames.BorderLeftColor;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderColor,
            PropertyNames.BorderLeft,
        };

        public static IValueConverter Converter = Or(CurrentColorConverter, AssignInitial(InitialValues.BorderLeftColorDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
