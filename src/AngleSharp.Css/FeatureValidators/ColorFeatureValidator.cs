namespace AngleSharp.Css.FeatureValidators
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    sealed class ColorFeatureValidator : IFeatureValidator
    {
        public Boolean Validate(IMediaFeature feature, IRenderDevice renderDevice)
        {
            var defaultValue = new Length(1.0, Length.Unit.None);
            var converter = feature.IsMinimum || feature.IsMaximum ? PositiveIntegerConverter : PositiveIntegerConverter.Option(defaultValue);
            var color = converter.Convert(feature.Value);

            if (color != null)
            {
                var desired = color.AsInt32();
                var available = Math.Pow(renderDevice.ColorBits, 2);

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
