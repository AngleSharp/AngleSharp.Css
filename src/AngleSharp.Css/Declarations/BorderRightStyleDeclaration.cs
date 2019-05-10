namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class BorderRightStyleDeclaration
    {
        public static String Name = PropertyNames.BorderRightStyle;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderStyle,
            PropertyNames.BorderRight,
        };

        public static IValueConverter Converter = Or(LineStyleConverter, AssignInitial(InitialValues.BorderRightStyleDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
