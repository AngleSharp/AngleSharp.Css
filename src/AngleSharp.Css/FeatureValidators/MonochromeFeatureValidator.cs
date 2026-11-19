namespace AngleSharp.Css.FeatureValidators
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    sealed class MonochromeFeatureValidator : IFeatureValidator
    {
        public Boolean Validate(IMediaFeature feature, IRenderDevice renderDevice)
        {
            // the bits per color component (or of a monochrome pixel), 0 if the device has none
            var available = renderDevice.MonochromeBits;

            if (!feature.HasValue)
            {
                return !feature.IsMinimum && !feature.IsMaximum && available > 0;
            }

            var value = NaturalIntegerConverter.Convert(feature);

            if (value != null)
            {
                var desired = value.AsInt32();

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
