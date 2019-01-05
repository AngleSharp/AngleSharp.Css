namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class TextIndentDeclaration
    {
        public static String Name = PropertyNames.TextIndent;

        public static IValueConverter Converter = Or(LengthOrPercentConverter, AssignInitial(Length.Zero));

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Animatable;
    }
}
