namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class OpacityDeclaration
    {
        public static String Name = PropertyNames.Opacity;

        public static IValueConverter Converter = Or(NumberConverter, AssignInitial(InitialValues.OpacityDecl));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
