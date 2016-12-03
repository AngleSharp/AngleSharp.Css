namespace AngleSharp.Css.FeatureValidators
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    sealed class OrientationFeatureValidator : IFeatureValidator
    {
        public Boolean Validate(IMediaFeature feature, IRenderDevice device)
        {
            var portrait = OrientationModeConverter.Convert(feature.Value);

            if (portrait != null)
            {
                var desired = portrait.Is(CssKeywords.Portrait);
                var available = device.DeviceHeight >= device.DeviceWidth;
                return desired == available;
            }

            return false;
        }
    }
}
