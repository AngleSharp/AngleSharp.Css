namespace AngleSharp.Css.FeatureValidators
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;

    sealed class HoverFeatureValidator : IFeatureValidator
    {
        #region Fields

        private static readonly IValueConverter TheConverter = Map.HoverAbilities.ToConverter();

        #endregion
        
        #region Methods

        public Boolean Validate(IMediaFeature feature, IRenderDevice device)
        {
            var hover = TheConverter.Convert(feature.Value);

            if (hover != null)
            {
                var desired = hover.AsEnum<HoverAbility>();
                //Nothing yet, so we assume we have a headless browser
                return desired == HoverAbility.None;
            }

            return false;
        }

        #endregion
    }
}
