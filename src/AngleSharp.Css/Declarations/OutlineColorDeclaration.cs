namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class OutlineColorDeclaration
    {
        public static String Name = PropertyNames.OutlineColor;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Outline,
        };

        public static IValueConverter Converter = Or(InvertedColorConverter, AssignInitial(InitialValues.OutlineColorDecl));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
