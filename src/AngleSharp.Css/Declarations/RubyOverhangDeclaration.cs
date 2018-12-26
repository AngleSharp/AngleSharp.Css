namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class RubyOverhangDeclaration
    {
        public static String Name = PropertyNames.RubyOverhang;

        public static IValueConverter Converter = Or(RubyOverhangModeConverter, AssignInitial(RubyOverhangMode.None));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
