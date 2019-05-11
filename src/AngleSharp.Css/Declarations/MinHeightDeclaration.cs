namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class MinHeightDeclaration
    {
        public static String Name = PropertyNames.MinHeight;

        public static IValueConverter Converter = Or(LengthOrPercentConverter, AssignInitial(InitialValues.MinHeightDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
