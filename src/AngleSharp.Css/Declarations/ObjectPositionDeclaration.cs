namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class ObjectPositionDeclaration
    {
        public static String Name = PropertyNames.ObjectPosition;

        public static IValueConverter Converter = Or(PointConverter, AssignInitial(InitialValues.ObjectPositionDecl));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
