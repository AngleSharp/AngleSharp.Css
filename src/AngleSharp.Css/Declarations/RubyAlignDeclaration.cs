namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class RubyAlignDeclaration
    {
        public static String Name = PropertyNames.RubyAlign;

        public static IValueConverter Converter = RubyAlignmentConverter;

        public static ICssValue InitialValue = InitialValues.RubyAlignDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
