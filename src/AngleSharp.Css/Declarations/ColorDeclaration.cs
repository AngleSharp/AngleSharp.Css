namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class ColorDeclaration
    {
        public static String Name = PropertyNames.Color;

        public static IValueConverter Converter = Or(ColorConverter, AssignInitial(Color.Black));

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Hashless | PropertyFlags.Animatable;
    }
}
