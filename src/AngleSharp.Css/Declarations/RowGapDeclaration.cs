namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class RowGapDeclaration
    {
        public static String Name = PropertyNames.RowGap;

        public static readonly String[] Shorthands = new[]
        {
            PropertyNames.Gap,
        };

        public static IValueConverter Converter = GapConverter;

        public static ICssValue InitialValue = InitialValues.RowGapDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
