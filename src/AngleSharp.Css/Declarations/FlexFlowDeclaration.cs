namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class FlexFlowDeclaration
    {
        public static String Name = PropertyNames.FlexFlow;

        public static String[] Longhands = new[]
        {
            PropertyNames.FlexDirection,
            PropertyNames.FlexWrap,
        };

        public static IValueConverter Converter = Or(NumberConverter, AssignInitial(0));

        public static PropertyFlags Flags = PropertyFlags.Shorthand;
    }
}
