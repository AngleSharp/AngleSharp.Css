namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Linq;
    using static ValueConverters;

    static class TransitionDeclaration
    {
        public static String Name = PropertyNames.Transition;

        public static IValueConverter Converter = new TransitionAggregator();

        public static PropertyFlags Flags = PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.TransitionProperty,
            PropertyNames.TransitionDuration,
            PropertyNames.TransitionTimingFunction,
            PropertyNames.TransitionDelay,
        };

        sealed class TransitionValueConverter : IValueConverter
        {
            private static readonly IValueConverter converter = WithAny(
                AnimatableConverter.Option(),
                TimeConverter.Option(),
                TransitionConverter.Option(),
                TimeConverter.Option()).FromList();

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }
        }

        sealed class TransitionAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(new TransitionValueConverter(), AssignInitial());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Merge(ICssValue[] values)
            {
                var animatable = values[0] as CssListValue;
                var duration = values[1] as CssListValue;
                var timing = values[2] as CssListValue;
                var delay = values[3] as CssListValue;

                if (animatable != null || duration != null || timing != null || delay != null)
                {
                    return CreateValue(animatable, duration, timing, delay);
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                var list = value as CssListValue;

                if (list != null)
                {
                    return new[]
                    {
                        CreateMultiple(list, 0),
                        CreateMultiple(list, 1),
                        CreateMultiple(list, 2),
                        CreateMultiple(list, 3),
                    };
                }

                return null;
            }

            private static ICssValue CreateMultiple(CssListValue transition, Int32 index)
            {
                var values = transition.Items.OfType<CssTupleValue>().Select(m => m.Items[index]);

                if (values.Any())
                {
                    return new CssListValue(values.ToArray());
                }

                return null;
            }

            private static ICssValue CreateValue(CssListValue animatable, CssListValue duration, CssListValue timing, CssListValue delay)
            {
                var items = (animatable ?? duration ?? timing ?? delay).Items;
                var layers = new ICssValue[items.Length];

                for (var i = 0; i < items.Length; i++)
                {
                    layers[i] = new CssTupleValue(new[]
                    {
                        GetValue(animatable, i),
                        GetValue(duration, i),
                        GetValue(timing, i),
                        GetValue(delay, i),
                    });
                }

                return new CssListValue(layers);
            }

            private static ICssValue GetValue(CssListValue container, Int32 index)
            {
                if (container != null && index < container.Items.Length)
                {
                    return container.Items[index];
                }

                return null;
            }
        }
    }
}
