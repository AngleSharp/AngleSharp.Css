namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ClearDeclaration
    {
        public static String Name = PropertyNames.Clear;

        public static IValueConverter Converter = Or(ClearModeConverter, AssignInitial(ClearMode.None));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
