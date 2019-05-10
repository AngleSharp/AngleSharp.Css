namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class DirectionDeclaration
    {
        public static String Name = PropertyNames.Direction;

        public static IValueConverter Converter = Or(DirectionModeConverter, AssignInitial(InitialValues.DirectionDecl));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
