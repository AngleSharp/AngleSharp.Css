namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FontStretchDeclaration
    {
        public static String Name = PropertyNames.FontStretch;

        public static IValueConverter Converter = Or(FontStretchConverter, AssignInitial(FontStretch.Normal));

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Animatable;
    }
}
