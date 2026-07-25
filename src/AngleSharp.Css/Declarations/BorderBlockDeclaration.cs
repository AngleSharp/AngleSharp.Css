#nullable disable
namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System.Linq;
    using System;
    using static ValueConverters;

    static class BorderBlockDeclaration
    {
        public static String Name = PropertyNames.BorderBlock;

        public static IValueConverter Converter = new BorderBlockAggregator();

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderBlockWidth,
            PropertyNames.BorderBlockStyle,
            PropertyNames.BorderBlockColor,
        };

        sealed class BorderBlockAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = WithAny(
                Or(LineWidthConverter, VarConverter).Option(InitialValues.BorderTopWidthDecl),
                Or(LineStyleConverter, VarConverter).Option(InitialValues.BorderTopStyleDecl),
                Or(CurrentColorConverter, VarConverter).Option(InitialValues.BorderTopColorDecl));

            public ICssValue Convert(StringSource source) => converter.Convert(source);

            public ICssValue Merge(ICssValue[] values)
            {
                var simplified = new ICssValue[values.Length];

                for (var i = 0; i < values.Length; i++)
                {
                    if (values[i] is CssFlowRelativeValue fr)
                    {
                        // Only mergeable as a single shorthand when both sides are equal
                        if (fr.Start?.CssText != fr.End?.CssText)
                        {
                            return null;
                        }

                        simplified[i] = fr.Start;
                    }
                    else
                    {
                        simplified[i] = values[i];
                    }
                }

                return new CssTupleValue(simplified);
            }

            public ICssValue[] Split(ICssValue value)
            {
                if (value is CssTupleValue options)
                {
                    return options.ToArray();
                }

                return null;
            }
        }
    }
}
