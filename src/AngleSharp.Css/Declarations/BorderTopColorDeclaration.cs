namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class BorderTopColorDeclaration
    {
        public static String Name = PropertyNames.BorderTopColor;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderColor,
            PropertyNames.BorderTop,
        };

        public static IValueConverter Converter = Or(CurrentColorConverter, AssignInitial(InitialValues.BorderTopColorDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
