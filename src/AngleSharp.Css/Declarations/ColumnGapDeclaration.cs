namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class ColumnGapDeclaration
    {
        public static String Name = PropertyNames.ColumnGap;

        public static readonly String[] Shorthands = new[]
        {
            PropertyNames.Gap,
        };

        public static IValueConverter Converter = Or(GapConverter, AssignInitial(InitialValues.ColumnGapDecl));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
