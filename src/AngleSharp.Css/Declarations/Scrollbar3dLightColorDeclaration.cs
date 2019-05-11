namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class Scrollbar3dLightColorDeclaration
    {
        public static String Name = PropertyNames.Scrollbar3dLightColor;

        public static IValueConverter Converter = Or(ColorConverter, AssignInitial(InitialValues.Scrollbar3dLightColorDecl));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
