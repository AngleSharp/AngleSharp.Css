namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Dom;
    using System;
    using static ValueConverters;

    static class TextAlignDeclaration
    {
        public static String Name = PropertyNames.TextAlign;

        public static IValueConverter Converter = Or(HorizontalAlignmentConverter, AssignInitial(HorizontalAlignment.Left));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
