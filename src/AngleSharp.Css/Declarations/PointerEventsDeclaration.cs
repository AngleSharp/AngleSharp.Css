namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class PointerEventsDeclaration
    {
        public static String Name = PropertyNames.PointerEvents;

        public static IValueConverter Converter = Or(PointerEventConverter, AssignInitial(InitialValues.PointerEventsDecl));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
