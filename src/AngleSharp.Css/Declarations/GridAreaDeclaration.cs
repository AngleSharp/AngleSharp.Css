namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class GridAreaDeclaration
    {
        public static readonly String Name = PropertyNames.GridArea;

        public static readonly String[] Longhands = new[]
        {
            PropertyNames.GridRowStart,
            PropertyNames.GridColumnStart,
            PropertyNames.GridRowEnd,
            PropertyNames.GridColumnEnd,
        };

        public static readonly IValueConverter Converter = new GridAreaAggregator();

        public static readonly PropertyFlags Flags = PropertyFlags.Shorthand;

        sealed class GridAreaAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(
                SlashSeparated(Or(
                    Assign(CssKeywords.Auto, "auto"),
                    WithAny(Assign(CssKeywords.Span, true), IntegerConverter, IdentifierConverter),
                    WithAny(IntegerConverter, IdentifierConverter),
                    IdentifierConverter)).Many(1, 4),
                AssignInitial());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Merge(ICssValue[] values)
            {
                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                return null;
            }
        }
    }
}
