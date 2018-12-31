namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static ValueConverters;

    static class AnimationDeclaration
    {
        public static String Name = PropertyNames.Animation;

        public static IValueConverter Converter = new AnimationAggregator();

        public static PropertyFlags Flags = PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.AnimationDuration,
            PropertyNames.AnimationTimingFunction,
            PropertyNames.AnimationDelay,
            PropertyNames.AnimationIterationCount,
            PropertyNames.AnimationDirection,
            PropertyNames.AnimationFillMode,
            PropertyNames.AnimationPlayState,
            PropertyNames.AnimationName,
        };

        sealed class AnimationConverter : IValueConverter
        {
            private static readonly IValueConverter ListConverter = WithAny(
                TimeConverter.Option(),
                TransitionConverter.Option(),
                TimeConverter.Option(),
                PositiveOrInfiniteNumberConverter.Option(),
                AnimationDirectionConverter.Option(),
                AnimationFillStyleConverter.Option(),
                PlayStateConverter.Option(),
                IdentifierConverter.Option()).FromList();

            public ICssValue Convert(StringSource source)
            {
                return ListConverter.Convert(source);
            }
        }

        sealed class AnimationAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(new AnimationConverter(), AssignInitial());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Merge(ICssValue[] values)
            {
                var duration = values[0] as CssListValue;
                var timing = values[1] as CssListValue;
                var delay = values[2] as CssListValue;
                var iterationCount = values[3] as CssListValue;
                var direction = values[4] as CssListValue;
                var fillMode = values[5] as CssListValue;
                var playState = values[6] as CssListValue;
                var name = values[7] as CssListValue;

                if (duration != null || timing != null || delay != null || iterationCount != null || direction != null || fillMode != null || playState != null || name != null)
                {
                    return CreateValue(duration, timing, delay, iterationCount, direction, fillMode, playState, name);
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
                        CreateMultiple(list, 4),
                        CreateMultiple(list, 5),
                        CreateMultiple(list, 6),
                        CreateMultiple(list, 7),
                    };
                }

                return null;
            }

            private static ICssValue CreateMultiple(CssListValue animation, Int32 index)
            {
                var values = animation.Items.OfType<CssTupleValue>().Select(m => m.Items[index]);

                if (values.Any())
                {
                    return new CssListValue(values.ToArray());
                }

                return null;
            }

            private static ICssValue CreateValue(CssListValue duration, CssListValue timing, CssListValue delay, CssListValue iterationCount, CssListValue direction, CssListValue fillMode, CssListValue playState, CssListValue name)
            {
                var items = (duration ?? timing ?? delay ?? iterationCount ?? direction ?? fillMode ?? playState ?? name).Items;
                var layers = new ICssValue[items.Length];

                for (var i = 0; i < items.Length; i++)
                {
                    layers[i] = new CssTupleValue(new[]
                    {
                        GetValue(duration, i),
                        GetValue(timing, i),
                        GetValue(delay, i),
                        GetValue(iterationCount, i),
                        GetValue(direction, i),
                        GetValue(fillMode, i),
                        GetValue(playState, i),
                        GetValue(name, i),
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
