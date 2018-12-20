namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class PerspectiveDeclaration
    {
        public static String Name = PropertyNames.Perspective;

        public static IValueConverter Converter = Or(LengthConverter, None, AssignInitial(Length.Zero));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
