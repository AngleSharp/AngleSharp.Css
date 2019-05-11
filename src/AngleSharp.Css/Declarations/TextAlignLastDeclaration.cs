namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class TextAlignLastDeclaration
    {
        public static String Name = PropertyNames.TextAlignLast;

        public static IValueConverter Converter = Or(TextAlignLastConverter, AssignInitial(InitialValues.TextAlignLastDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
