namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class TextIndentDeclaration
    {
        public static String Name = PropertyNames.TextIndent;

        public static IValueConverter Converter = Or(LengthOrPercentConverter, AssignInitial(InitialValues.TextIndentDecl));

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Animatable;
    }
}
