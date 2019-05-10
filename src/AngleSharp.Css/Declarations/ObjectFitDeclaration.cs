namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class ObjectFitDeclaration
    {
        public static String Name = PropertyNames.ObjectFit;

        public static IValueConverter Converter = Or(ObjectFittingConverter, AssignInitial(InitialValues.ObjectFitDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
