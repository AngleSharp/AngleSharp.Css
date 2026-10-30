namespace AngleSharp.Css.Dom
{
    using AngleSharp.Text;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    static class MediaListExtensions
    {
        private readonly static ConditionalWeakTable<IMediaFeature, IFeatureValidator> AssociatedValidators =
            new();

        private readonly static String[] KnownTypes =
        {
            // Intended for non-paged computer screens.
            CssKeywords.Screen,
            // Intended for speech synthesizers.
            CssKeywords.Speech,
            // Intended for paged material and for documents viewed on screen in print preview mode.
            CssKeywords.Print,
            // Suitable for all devices.
            CssKeywords.All
        };

        public static void AssociateValidator(this IMediaFeature feature, IFeatureValidator validator) =>
            AssociatedValidators.Add(feature, validator);

        public static Boolean Validate(this IMediaFeature feature, IRenderDevice device)
        {
            if (feature is MediaCondition condition)
            {
                return condition.Medium.Validate(device);
            }

            AssociatedValidators.TryGetValue(feature, out var validator);

            if (validator is not null && feature is MediaFeature { Comparisons: { } comparisons } range)
            {
                for (var i = 0; i < comparisons.Count; i++)
                {
                    if (!validator.Validate(range, i, device))
                    {
                        return false;
                    }
                }

                return true;
            }

            return validator?.Validate(feature, device) ?? false;
        }

        /// <summary>
        /// Evaluates a comparison of the range syntax with what the validators know: the min- and
        /// max- prefixed (inclusive) forms of the feature and the plain one (equality).
        /// </summary>
        private static Boolean Validate(this IFeatureValidator validator, MediaFeature feature, Int32 index, IRenderDevice device)
        {
            Boolean AtLeast() => validator.Validate(feature.GetBound(index, "min-"), device);
            Boolean AtMost() => validator.Validate(feature.GetBound(index, "max-"), device);
            Boolean Equal() => validator.Validate(feature.GetBound(index, String.Empty), device);

            return feature.Comparisons[index].Operator switch
            {
                ">=" => AtLeast(),
                "<=" => AtMost(),
                ">" => AtLeast() && !Equal(),
                "<" => AtMost() && !Equal(),
                _ => Equal(),
            };
        }

        public static Boolean Validate(this IMediaList list, IRenderDevice device) => !list.Any() || list.Any(m => m.Validate(device));

        public static Boolean Validate(this ICssMedium medium, IRenderDevice device)
        {
            // `not` negates the whole query, i.e. the media type and all features together
            var matches = medium.MatchesType(device) && medium.MatchesFeatures(device);
            return matches != medium.IsInverse;
        }

        private static Boolean MatchesType(this ICssMedium medium, IRenderDevice device)
        {
            var type = medium.Type;

            if (String.IsNullOrEmpty(type) || type.Is(CssKeywords.All))
            {
                return true;
            }

            if (!KnownTypes.Any(m => m.Is(type)))
            {
                return false;
            }

            return device is null ||
                (type.Is(CssKeywords.Screen) && device.Category == DeviceCategory.Screen) ||
                (type.Is(CssKeywords.Speech) && device.Category == DeviceCategory.Speech) ||
                (type.Is(CssKeywords.Print) && device.Category == DeviceCategory.Printer);
        }

        private static Boolean MatchesFeatures(this ICssMedium medium, IRenderDevice device) =>
            medium is CssMedium { Connector: var connector } && connector.Is(CssKeywords.Or)
                ? medium.Features.Any(m => m.Validate(device))
                : medium.Features.All(m => m.Validate(device));
    }
}
