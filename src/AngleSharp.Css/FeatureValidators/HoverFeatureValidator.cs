namespace AngleSharp.Css.FeatureValidators
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    sealed class HoverFeatureValidator : IFeatureValidator
    {
        public Boolean Validate(IMediaFeature feature, IRenderDevice device)
        {
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
