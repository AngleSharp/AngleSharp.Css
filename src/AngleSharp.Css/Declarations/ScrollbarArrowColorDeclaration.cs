namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class ScrollbarArrowColorDeclaration
    {
        public static String Name = PropertyNames.ScrollbarArrowColor;

        public static IValueConverter Converter = Or(ColorConverter, AssignInitial(Colors.GetColor("buttontext")));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
