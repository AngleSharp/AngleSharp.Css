namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class BorderImageRepeatDeclaration
    {
        public static String Name = PropertyNames.BorderImageRepeat;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderImage,
        };

        public static IValueConverter Converter = Or(BorderImageRepeatConverter, AssignInitial(InitialValues.BorderImageRepeatDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
