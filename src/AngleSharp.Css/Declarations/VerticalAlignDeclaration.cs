namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Dom;
    using System;
    using static ValueConverters;

    static class VerticalAlignDeclaration
    {
        public static String Name = PropertyNames.VerticalAlign;

        public static IValueConverter Converter = Or(LengthOrPercentConverter, VerticalAlignmentConverter, AssignInitial(VerticalAlignment.Baseline));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
