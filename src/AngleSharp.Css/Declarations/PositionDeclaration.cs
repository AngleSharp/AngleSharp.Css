namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class PositionDeclaration
    {
        public static String Name = PropertyNames.Position;

        public static IValueConverter Converter = Or(PositionModeConverter, AssignInitial(PositionMode.Static));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
