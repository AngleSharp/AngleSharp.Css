namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class AlignSelfDeclaration
    {
        public static String Name = PropertyNames.AlignSelf;

        public static IValueConverter Converter = Or(AlignSelfConverter, AssignInitial(FlexContentMode.Auto));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
