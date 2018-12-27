namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FlexDirectionDeclaration
    {
        public static String Name = PropertyNames.FlexDirection;

        public static String[] Shorthands = new[]
        {
            PropertyNames.FlexFlow,
        };

        public static IValueConverter Converter = Or(FlexDirectionConverter, AssignInitial(FlexDirection.Row));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
