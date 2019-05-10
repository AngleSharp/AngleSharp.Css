namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class FloatDeclaration
    {
        public static String Name = PropertyNames.Float;

        public static IValueConverter Converter = Or(FloatingConverter, AssignInitial(InitialValues.FloatDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
