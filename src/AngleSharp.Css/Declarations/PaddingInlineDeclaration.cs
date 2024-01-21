namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class PaddingInlineDeclaration
    {
        public static String Name = PropertyNames.PaddingInline;

        public static IValueConverter Converter = new PaddingInlineAggregator();

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.Shorthand;

        public static String[] Longhands =
        [
            PropertyNames.PaddingInlineStart,
            PropertyNames.PaddingInlineEnd,
        ];

        sealed class PaddingInlineAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(AutoLengthOrPercentConverter, AssignInitial(CssLengthValue.Zero)).FlowRelative();

            public ICssValue Convert(StringSource source) => converter.Convert(source);

            public ICssValue Merge(ICssValue[] values)
            {
                var start = values[0];
                var end = values[1];

                if (start != null && end != null)
                {
                    return new CssFlowRelativeValue([start, end]);
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                if (value is CssFlowRelativeValue flowRelative)
                {
                    return [flowRelative.Start, flowRelative.End];
                }

                return null;
            }
        }
    }
}