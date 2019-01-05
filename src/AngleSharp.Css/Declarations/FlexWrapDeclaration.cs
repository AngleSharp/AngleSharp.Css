namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FlexWrapDeclaration
    {
        public static String Name = PropertyNames.FlexWrap;

        public static String[] Shorthands = new[]
        {
            PropertyNames.FlexFlow,
        };

        public static IValueConverter Converter = Or(FlexWrapConverter, AssignInitial(FlexWrapMode.NoWrap));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
