namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class RowGapDeclaration
    {
        public static String Name = PropertyNames.RowGap;

        public static readonly String[] Shorthands = new[]
        {
            PropertyNames.Gap,
        };

        public static IValueConverter Converter = Or(GapConverter, AssignInitial(Length.Normal));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
