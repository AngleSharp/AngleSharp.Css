#nullable disable
namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class AnimationTimelineDeclaration
    {
        public static String Name = PropertyNames.AnimationTimeline;

        public static IValueConverter Converter = AnimationTimelineConverter;

        public static ICssValue InitialValue = InitialValues.AnimationTimelineDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
