namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class LineBreakDeclaration
    {
        public static String Name = PropertyNames.LineBreak;

        public static IValueConverter Converter = LineBreakConverter;

        public static ICssValue InitialValue = InitialValues.LineBreakDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
