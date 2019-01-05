namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class BorderImageSliceDeclaration
    {
        public static String Name = PropertyNames.BorderImageSlice;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderImage,
        };

        public static IValueConverter Converter = Or(BorderImageSliceConverter, AssignInitial(Length.Full));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
