namespace AngleSharp.Css.FeatureValidators
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    sealed class ColorIndexFeatureValidator : IFeatureValidator
    {
        public Boolean Validate(IMediaFeature feature, IRenderDevice device)
        {
            var converter = feature.IsMinimum || feature.IsMaximum ? NaturalIntegerConverter : NaturalIntegerConverter.Option(1);
            var index = converter.Convert(feature.Value);

            if (index != null)
            {
                var desired = index.AsInteger();
                var available = device.ColorBits;

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
