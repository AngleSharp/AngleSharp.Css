namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class JustifyContentDeclaration
    {
        public static String Name = PropertyNames.JustifyContent;

        public static IValueConverter Converter = Or(JustifyContentConverter, AssignInitial(FlexContentMode.Start));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
