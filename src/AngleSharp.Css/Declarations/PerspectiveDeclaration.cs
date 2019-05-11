namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class PerspectiveDeclaration
    {
        public static String Name = PropertyNames.Perspective;

        public static IValueConverter Converter = Or(LengthConverter, None, AssignInitial(InitialValues.PerspectiveDecl));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
