namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class RubyPositionDeclaration
    {
        public static String Name = PropertyNames.RubyPosition;

        public static IValueConverter Converter = Or(RubyPositionConverter, AssignInitial(InitialValues.RubyPositionDecl));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
