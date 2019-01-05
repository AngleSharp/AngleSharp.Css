namespace AngleSharp.Css.FeatureValidators
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    sealed class UpdateFrequencyFeatureValidator : IFeatureValidator
    {
        public Boolean Validate(IMediaFeature feature, IRenderDevice device)
        {
            var frequency = UpdateFrequencyConverter.Convert(feature.Value);

            if (frequency != null)
            {
                var desired = frequency.AsEnum<UpdateFrequency>();
                var available = device.Frequency;

                if (available >= 30)
                {
                    return desired == UpdateFrequency.Normal;
                }
                else if (available > 0)
                {
                    return desired == UpdateFrequency.Slow;
                }

                return desired == UpdateFrequency.None;
            }

            return false;
        }
    }
}
