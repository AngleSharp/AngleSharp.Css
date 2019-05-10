namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class AlignSelfDeclaration
    {
        public static String Name = PropertyNames.AlignSelf;

        public static IValueConverter Converter = Or(AlignSelfConverter, AssignInitial(InitialValues.AlignSelfDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
