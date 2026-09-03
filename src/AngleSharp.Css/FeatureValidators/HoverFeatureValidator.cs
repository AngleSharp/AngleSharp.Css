namespace AngleSharp.Css.FeatureValidators
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    sealed class HoverFeatureValidator : IFeatureValidator
    {
        private readonly String _name;

        public HoverFeatureValidator(String name)
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

            var hover = HoverAbilityConverter.Convert(feature.Value);

            if (hover != null)
            {
                var desired = hover.AsEnum<HoverAbility>();
                //Nothing yet, so we assume we have a headless browser
                return desired == HoverAbility.None;
            }

            return false;
        }
    }
}
