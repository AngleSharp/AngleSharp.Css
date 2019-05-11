namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class GridRowGapDeclaration
    {
        public static readonly String Name = PropertyNames.GridRowGap;

        public static readonly String[] Shorthands = new[]
        {
            PropertyNames.GridGap,
        };

        public static readonly IValueConverter Converter = GapConverter;

        public static ICssValue InitialValue = InitialValues.GridRowGapDecl;

        public static readonly PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
