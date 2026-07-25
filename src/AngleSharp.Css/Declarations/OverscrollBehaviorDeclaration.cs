#nullable disable
namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class OverscrollBehaviorDeclaration
    {
        public static String Name = PropertyNames.OverscrollBehavior;

        public static IValueConverter Converter = new OverscrollBehaviorAggregator();

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.OverscrollBehaviorX,
            PropertyNames.OverscrollBehaviorY,
        };

        sealed class OverscrollBehaviorAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(OverscrollBehaviorConverter, AssignInitial(InitialValues.OverscrollBehaviorXDecl)).FlowRelative();

            public ICssValue Convert(StringSource source) => converter.Convert(source);

            public ICssValue Merge(ICssValue[] values)
            {
                var x = values[0];
                var y = values[1];

                if (x != null && y != null)
                {
                    return new CssFlowRelativeValue(new[] { x, y });
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                if (value is CssFlowRelativeValue flowRelative)
                {
                    return new[] { flowRelative.Start, flowRelative.End };
                }

                return null;
            }
        }
    }
}
