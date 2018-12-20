namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class BorderSpacingDeclaration
    {
        public static String Name = PropertyNames.BorderSpacing;

        public static IValueConverter Converter = Or(LengthConverter.Many(1, 2), AssignInitial(Length.Zero));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
