namespace AngleSharp.Css.FeatureValidators
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    sealed class ColorFeatureValidator : IFeatureValidator
    {
        public Boolean Validate(IMediaFeature feature, IRenderDevice device)
        {
            var converter = feature.IsMinimum || feature.IsMaximum ? PositiveIntegerConverter : PositiveIntegerConverter.Option(1);
            var color = converter.Convert(feature.Value);

            if (color != null)
            {
                var desired = color.AsInteger();
                var available = Math.Pow(device.ColorBits, 2);

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
