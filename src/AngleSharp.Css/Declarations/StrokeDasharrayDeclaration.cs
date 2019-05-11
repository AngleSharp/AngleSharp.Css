namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class StrokeDasharrayDeclaration
    {
        public static String Name = PropertyNames.StrokeDasharray;

        public static IValueConverter Converter = Or(StrokeDasharrayConverter, AssignInitial(InitialValues.StrokeDasharrayDecl));

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Unitless;
    }
}
