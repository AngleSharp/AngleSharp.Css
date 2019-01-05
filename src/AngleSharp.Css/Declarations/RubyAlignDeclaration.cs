namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class RubyAlignDeclaration
    {
        public static String Name = PropertyNames.RubyAlign;

        public static IValueConverter Converter = Or(RubyAlignmentConverter, AssignInitial(RubyAlignment.SpaceAround));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
