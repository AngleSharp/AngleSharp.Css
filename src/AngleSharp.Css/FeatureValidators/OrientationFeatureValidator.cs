namespace AngleSharp.Css.FeatureValidators
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    sealed class OrientationFeatureValidator : IFeatureValidator
    {
        #region Fields

        private static readonly IValueConverter TheConverter = Toggle(CssKeywords.Portrait, CssKeywords.Landscape);

        #endregion
        
        #region Methods

        public Boolean Validate(IMediaFeature feature, IRenderDevice device)
        {
            var portrait = TheConverter.Convert(feature.Value);

            if (portrait != null)
            {
                var desired = portrait.Is(CssKeywords.Portrait);
                var available = device.DeviceHeight >= device.DeviceWidth;
                return desired == available;
            }

            return false;
        }

        #endregion
    }
}
