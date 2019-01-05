namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Dom;
    using System;
    using static ValueConverters;

    static class DirectionDeclaration
    {
        public static String Name = PropertyNames.Direction;

        public static IValueConverter Converter = Or(DirectionModeConverter, AssignInitial(DirectionMode.Ltr));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
