namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class AlignItemsDeclaration
    {
        public static String Name = PropertyNames.AlignItems;

        public static IValueConverter Converter = Or(AlignItemsConverter, AssignInitial(FlexContentMode.Stretch));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
