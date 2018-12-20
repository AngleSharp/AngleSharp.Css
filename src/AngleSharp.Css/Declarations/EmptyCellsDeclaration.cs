namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class EmptyCellsDeclaration
    {
        public static String Name = PropertyNames.EmptyCells;

        public static IValueConverter Converter = Or(EmptyCellsConverter, AssignInitial(true));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
