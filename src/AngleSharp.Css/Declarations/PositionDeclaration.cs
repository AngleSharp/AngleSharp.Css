namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class PositionDeclaration
    {
        public static String Name = PropertyNames.Position;

        public static IValueConverter Converter = Or(PositionModeConverter, AssignInitial(InitialValues.PositionDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
