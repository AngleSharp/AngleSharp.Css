namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class StringSetDeclaration
    {
        public static String Name = PropertyNames.StringSet;

        public static IValueConverter Converter = Any;

        public static ICssValue InitialValue = InitialValues.StringSetDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
