namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class OutlineStyleDeclaration
    {
        public static String Name = PropertyNames.OutlineStyle;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Outline,
        };

        public static IValueConverter Converter = Or(LineStyleConverter, AssignInitial(InitialValues.OutlineStyleDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
