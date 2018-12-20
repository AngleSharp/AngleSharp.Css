namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class UnicodeBidiDeclaration
    {
        public static String Name = PropertyNames.UnicodeBidi;

        public static IValueConverter Converter = Or(UnicodeModeConverter, AssignInitial(UnicodeMode.Normal));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
