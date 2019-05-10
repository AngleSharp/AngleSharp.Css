namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class FlexWrapDeclaration
    {
        public static String Name = PropertyNames.FlexWrap;

        public static String[] Shorthands = new[]
        {
            PropertyNames.FlexFlow,
        };

        public static IValueConverter Converter = Or(FlexWrapConverter, AssignInitial(InitialValues.FlexWrapDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
