namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class BorderImageSliceDeclaration
    {
        public static String Name = PropertyNames.BorderImageSlice;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderImage,
        };

        public static IValueConverter Converter = Or(BorderImageSliceConverter, AssignInitial(InitialValues.BorderImageSliceDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
