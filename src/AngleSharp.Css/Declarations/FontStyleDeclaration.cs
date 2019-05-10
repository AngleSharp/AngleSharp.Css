namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class FontStyleDeclaration
    {
        public static String Name = PropertyNames.FontStyle;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Font,
        };

        public static IValueConverter Converter = Or(FontStyleConverter, AssignInitial(InitialValues.FontStyleDecl));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
