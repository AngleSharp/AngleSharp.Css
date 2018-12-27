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
            PropertyNames.AnimationName,
            PropertyNames.AnimationDuration,
            PropertyNames.AnimationTimingFunction,
            PropertyNames.AnimationDelay,
            PropertyNames.AnimationIterationCount,
            PropertyNames.AnimationDirection,
            PropertyNames.AnimationFillMode,
            PropertyNames.AnimationPlayState,
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

            public ICssValue Collect(IEnumerable<ICssProperty> properties)
            {
                var duration = properties.Where(m => m.Name == AnimationDurationDeclaration.Name).Select(m => m.RawValue).FirstOrDefault() as Multiple;
                var timing = properties.Where(m => m.Name == AnimationTimingFunctionDeclaration.Name).Select(m => m.RawValue).FirstOrDefault() as Multiple;
                var delay = properties.Where(m => m.Name == AnimationDelayDeclaration.Name).Select(m => m.RawValue).FirstOrDefault() as Multiple;
                var iterationCount = properties.Where(m => m.Name == AnimationIterationCountDeclaration.Name).Select(m => m.RawValue).FirstOrDefault() as Multiple;
                var direction = properties.Where(m => m.Name == AnimationDirectionDeclaration.Name).Select(m => m.RawValue).FirstOrDefault() as Multiple;
                var fillMode = properties.Where(m => m.Name == AnimationFillModeDeclaration.Name).Select(m => m.RawValue).FirstOrDefault() as Multiple;
                var playState = properties.Where(m => m.Name == AnimationPlayStateDeclaration.Name).Select(m => m.RawValue).FirstOrDefault() as Multiple;
                var name = properties.Where(m => m.Name == AnimationNameDeclaration.Name).Select(m => m.RawValue).FirstOrDefault() as Multiple;

                if (duration != null || timing != null || delay != null || iterationCount != null || direction != null || fillMode != null || playState != null || name != null)
                {
                    return CreateValue(duration, timing, delay, iterationCount, direction, fillMode, playState, name);
                }

                return null;
            }

            public IEnumerable<ICssProperty> Distribute(ICssValue value)
            {
                var list = value as Multiple;

                if (list != null)
                {
                    return new[]
                    {
                        new CssProperty(AnimationDurationDeclaration.Name, AnimationDurationDeclaration.Converter, AnimationDurationDeclaration.Flags, CreateMultiple(list, 0)),
                        new CssProperty(AnimationTimingFunctionDeclaration.Name, AnimationTimingFunctionDeclaration.Converter, AnimationTimingFunctionDeclaration.Flags, CreateMultiple(list, 1)),
                        new CssProperty(AnimationDelayDeclaration.Name, AnimationDelayDeclaration.Converter, AnimationDelayDeclaration.Flags, CreateMultiple(list, 2)),
                        new CssProperty(AnimationIterationCountDeclaration.Name, AnimationIterationCountDeclaration.Converter, AnimationIterationCountDeclaration.Flags, CreateMultiple(list, 3)),
                        new CssProperty(AnimationDirectionDeclaration.Name, AnimationDirectionDeclaration.Converter, AnimationDirectionDeclaration.Flags, CreateMultiple(list, 4)),
                        new CssProperty(AnimationFillModeDeclaration.Name, AnimationFillModeDeclaration.Converter, AnimationFillModeDeclaration.Flags, CreateMultiple(list, 5)),
                        new CssProperty(AnimationPlayStateDeclaration.Name, AnimationPlayStateDeclaration.Converter, AnimationPlayStateDeclaration.Flags, CreateMultiple(list, 6)),
                        new CssProperty(AnimationNameDeclaration.Name, AnimationNameDeclaration.Converter, AnimationNameDeclaration.Flags, CreateMultiple(list, 7)),
                    };
                }

                return null;
            }

            private static ICssValue CreateMultiple(Multiple animation, Int32 index)
            {
                var values = animation.Values.OfType<OrderedOptions>().Select(m => m.Options[index]);

                if (values.Any())
                {
                    return new Multiple(values.ToArray());
                }

                return null;
            }

            private static ICssValue CreateValue(Multiple duration, Multiple timing, Multiple delay, Multiple iterationCount, Multiple direction, Multiple fillMode, Multiple playState, Multiple name)
            {
                var items = (duration ?? timing ?? delay ?? iterationCount ?? direction ?? fillMode ?? playState ?? name).Values;
                var layers = new ICssValue[items.Length];

                for (var i = 0; i < items.Length; i++)
                {
                    layers[i] = new OrderedOptions(new[]
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

                return new Multiple(layers);
            }

            private static ICssValue GetValue(Multiple container, Int32 index)
            {
                if (container != null && index < container.Values.Length)
                {
                    return container.Values[index];
                }

                return null;
            }
        }
    }
}
