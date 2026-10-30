namespace AngleSharp.Css.FeatureValidators
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    sealed class OrientationFeatureValidator : IFeatureValidator
    {
        public Boolean Validate(IMediaFeature feature, IRenderDevice renderDevice)
        {
            var portrait = OrientationModeConverter.Convert(feature);

            if (portrait != null)
            {
                var desired = portrait.Is(CssKeywords.Portrait);
                // the orientation of the viewport, not the device (Media Queries 4, 4.4)
                var available = renderDevice.ViewPortHeight >= renderDevice.ViewPortWidth;
                return desired == available;
            }

            return false;
        }
    }
}
