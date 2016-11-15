namespace AngleSharp.Css.FeatureValidators
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;

    sealed class UpdateFrequencyFeatureValidator : IFeatureValidator
    {
        #region Fields

        private static readonly IValueConverter TheConverter = Map.UpdateFrequencies.ToConverter();

        #endregion
        
        #region Methods

        public Boolean Validate(IMediaFeature feature, IRenderDevice device)
        {
            var frequency = TheConverter.Convert(feature.Value);

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

        #endregion
    }
}
