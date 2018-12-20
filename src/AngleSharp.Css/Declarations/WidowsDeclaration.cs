namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class WidowsDeclaration
    {
        public static String Name = PropertyNames.Widows;

        public static IValueConverter Converter = Or(IntegerConverter, AssignInitial(2));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
