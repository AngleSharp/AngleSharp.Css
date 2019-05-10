namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class FontFamilyDeclaration
    {
        public static String Name = PropertyNames.FontFamily;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Font,
        };

        public static IValueConverter Converter = Or(FontFamiliesConverter, AssignInitial(InitialValues.FontFamilyDecl));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
