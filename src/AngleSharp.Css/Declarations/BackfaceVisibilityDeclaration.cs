namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class BackfaceVisibilityDeclaration
    {
        public static String Name = PropertyNames.BackfaceVisibility;

        public static IValueConverter Converter = Or(BackfaceVisibilityConverter, AssignInitial(true));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
