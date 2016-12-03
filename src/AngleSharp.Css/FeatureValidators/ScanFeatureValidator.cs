namespace AngleSharp.Css.FeatureValidators
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    sealed class ScanFeatureValidator : IFeatureValidator
    {
        public Boolean Validate(IMediaFeature feature, IRenderDevice device)
        {
            var interlace = ScanModeConverter.Convert(feature.Value);

            if (interlace != null)
            {
                var desired = interlace.Is(CssKeywords.Interlace);
                var available = device.IsInterlaced;
                return desired == available;
            }

            return false;
        }
    }
}
