namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class ScrollbarTrackColorDeclaration
    {
        public static String Name = PropertyNames.ScrollbarTrackColor;

        public static IValueConverter Converter = Or(ColorConverter, AssignInitial(Colors.GetColor("scrollbar")));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
