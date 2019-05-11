namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class GridColumnGapDeclaration
    {
        public static readonly String Name = PropertyNames.GridColumnGap;

        public static readonly String[] Shorthands = new[]
        {
            PropertyNames.GridGap,
        };

        public static readonly IValueConverter Converter = Or(GapConverter, AssignInitial(InitialValues.GridColumnGapDecl));

        public static readonly PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
