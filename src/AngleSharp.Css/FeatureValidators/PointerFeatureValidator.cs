namespace AngleSharp.Css.FeatureValidators
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;

    sealed class PointerFeatureValidator : IFeatureValidator
    {
        #region Fields

        private static readonly IValueConverter TheConverter = Map.PointerAccuracies.ToConverter();

        #endregion
        
        #region Methods

        public Boolean Validate(IMediaFeature feature, IRenderDevice device)
        {
            var accuracy = TheConverter.Convert(feature.Value);

            if (accuracy != null)
            {
                var desired = accuracy.AsEnum<PointerAccuracy>();
                //Nothing yet, so we assume we have a headless browser
                return desired == PointerAccuracy.None;
            }

            return false;
        }

        #endregion
    }
}
