namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class BackgroundColorDeclaration
    {
        public static String Name = PropertyNames.BackgroundColor;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Background,
        };

        public static IValueConverter Converter = Or(CurrentColorConverter, AssignInitial());

        public static PropertyFlags Flags = PropertyFlags.Hashless | PropertyFlags.Animatable;
    }
}
