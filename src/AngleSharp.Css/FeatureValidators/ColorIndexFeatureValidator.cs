namespace AngleSharp.Css.FeatureValidators
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    sealed class ColorIndexFeatureValidator : IFeatureValidator
    {
        public Boolean Validate(IMediaFeature feature, IRenderDevice renderDevice)
        {
            var defaultValue = new Length(1.0, Length.Unit.None);
            var converter = feature.IsMinimum || feature.IsMaximum ? NaturalIntegerConverter : NaturalIntegerConverter.Option(defaultValue);
            var index = converter.Convert(feature.Value);

            if (index != null)
            {
                var desired = index.AsInt32();
                var available = renderDevice.ColorBits;

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
