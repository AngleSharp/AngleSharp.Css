namespace AngleSharp.Css.FeatureValidators
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    sealed class AspectRatioFeatureValidator : IFeatureValidator
    {
        public Boolean Validate(IMediaFeature feature, IRenderDevice renderDevice)
        {
            var ratio = RatioConverter.Convert(feature.Value);

            if (ratio is not null)
            {
                var desired = ratio.AsDouble();
                var available = renderDevice.ViewPortWidth / (Double)renderDevice.ViewPortHeight;

                if (feature.IsMaximum)
                {
                    return available <= desired;
                }
                else if (feature.IsMinimum)
                {
                    return available >= desired;
                }

                return desired == available;
            }

            return false;
        }
    }
}
