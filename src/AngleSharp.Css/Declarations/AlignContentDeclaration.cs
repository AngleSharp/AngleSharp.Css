namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class AlignContentDeclaration
    {
        public static String Name = PropertyNames.AlignContent;

        public static IValueConverter Converter = Or(AlignContentConverter, AssignInitial(InitialValues.AlignContentDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
