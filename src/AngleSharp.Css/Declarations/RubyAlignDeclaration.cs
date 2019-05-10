namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class RubyAlignDeclaration
    {
        public static String Name = PropertyNames.RubyAlign;

        public static IValueConverter Converter = Or(RubyAlignmentConverter, AssignInitial(InitialValues.RubyAlignDecl));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
