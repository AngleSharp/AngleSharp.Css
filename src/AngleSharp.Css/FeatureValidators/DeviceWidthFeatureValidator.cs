namespace AngleSharp.Css.FeatureValidators
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    sealed class DeviceWidthFeatureValidator : IFeatureValidator
    {
        public Boolean Validate(IMediaFeature feature, IRenderDevice renderDevice)
        {
            var length = LengthConverter.Convert(feature.Value);

            if (length != null)
            {
                var desired = length.AsPx(renderDevice, RenderMode.Horizontal);
                var available = (Double)renderDevice.DeviceWidth;

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
