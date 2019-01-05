namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class BoxDecorationBreakDeclaration
    {
        public static String Name = PropertyNames.BoxDecorationBreak;

        public static IValueConverter Converter = Or(BoxDecorationConverter, AssignInitial(false));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
