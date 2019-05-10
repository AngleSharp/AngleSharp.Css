namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class StrokeLinecapDeclaration
    {
        public static String Name = PropertyNames.StrokeLinecap;

        public static IValueConverter Converter = Or(StrokeLinecapConverter, AssignInitial(InitialValues.StrokeLinecapDecl));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
