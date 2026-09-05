namespace AngleSharp.Css.FeatureValidators
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    sealed class PointerFeatureValidator : IFeatureValidator
    {
        private readonly String _name;

        public PointerFeatureValidator(String name)
        {
            _name = name;
        }

        public Boolean Validate(IMediaFeature feature, IRenderDevice renderDevice)
        {
            var preference = renderDevice.GetPreference(_name);

            if (preference is not null)
            {
                return PreferenceFeatureValidator.Matches(feature, preference, CssKeywords.None);
            }

            var accuracy = PointerAccuracyConverter.Convert(feature.Value);

            if (accuracy != null)
            {
                var desired = accuracy.AsEnum<PointerAccuracy>();
                //Nothing yet, so we assume we have a headless browser
                return desired == PointerAccuracy.None;
            }

            return false;
        }
    }
}
