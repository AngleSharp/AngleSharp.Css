namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class ColumnGapDeclaration
    {
        public static String Name = PropertyNames.ColumnGap;

        public static IValueConverter Converter = Or(LengthOrNormalConverter, AssignInitial(new Length(1f, Length.Unit.Em)));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
