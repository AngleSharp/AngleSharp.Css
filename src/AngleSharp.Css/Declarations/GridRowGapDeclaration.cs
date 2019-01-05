namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class GridRowGapDeclaration
    {
        public static readonly String Name = PropertyNames.GridRowGap;

        public static readonly String[] Shorthands = new[]
        {
            PropertyNames.GridGap,
        };

        public static readonly IValueConverter Converter = Or(GapConverter, AssignInitial(Length.Normal));

        public static readonly PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
