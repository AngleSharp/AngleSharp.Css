namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TransitionBehaviorDeclaration
    {
        public static String Name = PropertyNames.TransitionBehavior;

        public static IValueConverter Converter = TransitionBehaviorConverter.FromList();

        public static ICssValue InitialValue = InitialValues.TransitionBehaviorDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
