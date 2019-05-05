namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class PointerEventsDeclaration
    {
        public static String Name = PropertyNames.PointerEvents;

        public static IValueConverter Converter = Or(PointerEventConverter, AssignInitial(PointerEvent.Auto));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
