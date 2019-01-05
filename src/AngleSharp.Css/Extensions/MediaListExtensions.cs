namespace AngleSharp.Css.Dom
{
    using AngleSharp.Text;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    static class MediaListExtensions
    {
        private readonly static ConditionalWeakTable<IMediaFeature, IFeatureValidator> AssociatedValidators = new ConditionalWeakTable<IMediaFeature, IFeatureValidator>();

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

        public static void AssociateValidator(this IMediaFeature feature, IFeatureValidator validator)
        {
            AssociatedValidators.Add(feature, validator);
        }

        public static Boolean Validate(this IMediaFeature feature, IRenderDevice device)
        {
            var validator = default(IFeatureValidator);
            AssociatedValidators.TryGetValue(feature, out validator);
            return validator?.Validate(feature, device) ?? false;
        }

        public static Boolean Validate(this IMediaList list, IRenderDevice device)
        {
            return !list.Any(m => !m.Validate(device));
        }

        public static Boolean Validate(this ICssMedium medium, IRenderDevice device)
        {
            if (!String.IsNullOrEmpty(medium.Type) && KnownTypes.Contains(medium.Type) == medium.IsInverse)
            {
                return false;
            }

            return !medium.IsInvalid(device) && !medium.Features.Any(m => m.Validate(device) == medium.IsInverse);
        }

        private static Boolean IsInvalid(this ICssMedium medium, IRenderDevice device)
        {
            return medium.IsInvalid(device, CssKeywords.Screen, DeviceCategory.Screen) ||
                medium.IsInvalid(device, CssKeywords.Speech, DeviceCategory.Speech) ||
                medium.IsInvalid(device, CssKeywords.Print, DeviceCategory.Printer);
        }

        private static Boolean IsInvalid(this ICssMedium medium, IRenderDevice device, String keyword, DeviceCategory category)
        {
            return keyword.Is(medium.Type) && (device.Category == category) == medium.IsInverse;
        }
    }
}
