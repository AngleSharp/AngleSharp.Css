namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class BorderBottomColorDeclaration
    {
        public static String Name = PropertyNames.BorderBottomColor;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderColor,
            PropertyNames.BorderBottom,
        };

        public static IValueConverter Converter = Or(CurrentColorConverter, AssignInitial(InitialValues.BorderBottomColorDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
