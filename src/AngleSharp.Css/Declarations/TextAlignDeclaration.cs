namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class TextAlignDeclaration
    {
        public static String Name = PropertyNames.TextAlign;

        public static IValueConverter Converter = Or(HorizontalAlignmentConverter, AssignInitial(InitialValues.TextAlignDecl));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
