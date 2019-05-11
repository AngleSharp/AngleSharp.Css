namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class PerspectiveOriginDeclaration
    {
        public static String Name = PropertyNames.PerspectiveOrigin;

        public static IValueConverter Converter = Or(PointConverter, AssignInitial(InitialValues.PerspectiveOriginDecl));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
