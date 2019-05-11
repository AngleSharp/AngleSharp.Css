namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class UnicodeBidiDeclaration
    {
        public static String Name = PropertyNames.UnicodeBidi;

        public static IValueConverter Converter = UnicodeModeConverter;

        public static ICssValue InitialValue = InitialValues.UnicodeBidiDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
