namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class RubyPositionDeclaration
    {
        public static String Name = PropertyNames.RubyPosition;

        public static IValueConverter Converter = Or(RubyPositionConverter, AssignInitial(RubyPosition.Over));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
