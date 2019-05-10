namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class UnicodeBidiDeclaration
    {
        public static String Name = PropertyNames.UnicodeBidi;

        public static IValueConverter Converter = Or(UnicodeModeConverter, AssignInitial(InitialValues.UnicodeBidiDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
