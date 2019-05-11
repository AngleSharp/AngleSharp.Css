namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class RubyOverhangDeclaration
    {
        public static String Name = PropertyNames.RubyOverhang;

        public static IValueConverter Converter = RubyOverhangModeConverter;

        public static ICssValue InitialValue = InitialValues.RubyOverhangDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
