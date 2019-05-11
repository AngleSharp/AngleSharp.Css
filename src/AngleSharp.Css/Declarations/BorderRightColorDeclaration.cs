namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class BorderRightColorDeclaration
    {
        public static String Name = PropertyNames.BorderRightColor;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderColor,
            PropertyNames.BorderRight,
        };

        public static IValueConverter Converter = Or(CurrentColorConverter, AssignInitial(InitialValues.BorderRightColorDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
