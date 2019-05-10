namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class VisibilityDeclaration
    {
        public static String Name = PropertyNames.Visibility;

        public static IValueConverter Converter = Or(VisibilityConverter, AssignInitial(InitialValues.VisibilityDecl));

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Animatable;
    }
}
