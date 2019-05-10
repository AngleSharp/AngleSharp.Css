namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class RubyOverhangDeclaration
    {
        public static String Name = PropertyNames.RubyOverhang;

        public static IValueConverter Converter = Or(RubyOverhangModeConverter, AssignInitial(InitialValues.RubyOverhangDecl));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
