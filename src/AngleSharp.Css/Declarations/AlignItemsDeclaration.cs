namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class AlignItemsDeclaration
    {
        public static String Name = PropertyNames.AlignItems;

        public static IValueConverter Converter = Or(AlignItemsConverter, AssignInitial(InitialValues.AlignItemsDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
