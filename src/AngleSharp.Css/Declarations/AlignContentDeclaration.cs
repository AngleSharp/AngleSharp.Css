namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class AlignContentDeclaration
    {
        public static String Name = PropertyNames.AlignContent;

        public static IValueConverter Converter = Or(AlignContentConverter, AssignInitial(FlexContentMode.Stretch));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
