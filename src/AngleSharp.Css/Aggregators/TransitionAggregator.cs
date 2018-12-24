namespace AngleSharp.Css.Aggregators
{
    using AngleSharp.Css.Declarations;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    sealed class TransitionAggregator : IValueAggregator
    {
        public ICssValue Collect(IEnumerable<ICssProperty> properties)
        {
            var animatable = properties.Where(m => m.Name == TransitionPropertyDeclaration.Name).Select(m => m.RawValue).FirstOrDefault() as Multiple;
            var duration = properties.Where(m => m.Name == TransitionDurationDeclaration.Name).Select(m => m.RawValue).FirstOrDefault() as Multiple;
            var timing = properties.Where(m => m.Name == TransitionTimingFunctionDeclaration.Name).Select(m => m.RawValue).FirstOrDefault() as Multiple;
            var delay = properties.Where(m => m.Name == TransitionDelayDeclaration.Name).Select(m => m.RawValue).FirstOrDefault() as Multiple;

            if (animatable != null || duration != null || timing != null || delay != null)
            {
                return CreateValue(animatable, duration, timing, delay);
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
                    new CssProperty(TransitionPropertyDeclaration.Name, TransitionPropertyDeclaration.Converter, TransitionPropertyDeclaration.Flags, CreateMultiple(list, 0)),
                    new CssProperty(TransitionDurationDeclaration.Name, TransitionDurationDeclaration.Converter, TransitionDurationDeclaration.Flags, CreateMultiple(list, 1)),
                    new CssProperty(TransitionTimingFunctionDeclaration.Name, TransitionTimingFunctionDeclaration.Converter, TransitionTimingFunctionDeclaration.Flags, CreateMultiple(list, 2)),
                    new CssProperty(TransitionDelayDeclaration.Name, TransitionDelayDeclaration.Converter, TransitionDelayDeclaration.Flags, CreateMultiple(list, 3)),
                };
            }

            return null;
        }

        private static ICssValue CreateMultiple(Multiple transition, Int32 index)
        {
            var values = transition.Values.OfType<OrderedOptions>().Select(m => m.Options[index]);

            if (values.Any())
            {
                return new Multiple(values.ToArray());
            }

            return null;
        }

        private static ICssValue CreateValue(Multiple animatable, Multiple duration, Multiple timing, Multiple delay)
        {
            var items = (animatable ?? duration ?? timing ?? delay).Values;
            var layers = new ICssValue[items.Length];

            for (var i = 0; i < items.Length; i++)
            {
                layers[i] = new OrderedOptions(new[]
                {
                    GetValue(animatable, i),
                    GetValue(duration, i),
                    GetValue(timing, i),
                    GetValue(delay, i),
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