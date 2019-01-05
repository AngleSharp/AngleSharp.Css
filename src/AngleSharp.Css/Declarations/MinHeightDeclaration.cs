namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class MinHeightDeclaration
    {
        public static String Name = PropertyNames.MinHeight;

        public static IValueConverter Converter = Or(LengthOrPercentConverter, AssignInitial(Length.Zero));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
