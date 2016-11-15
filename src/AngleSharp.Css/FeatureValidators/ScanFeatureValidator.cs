namespace AngleSharp.Css.FeatureValidators
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    sealed class ScanFeatureValidator : IFeatureValidator
    {
        #region Fields

        private static readonly IValueConverter TheConverter = Toggle(CssKeywords.Interlace, CssKeywords.Progressive);

        #endregion

        #region Methods

        public Boolean Validate(IMediaFeature feature, IRenderDevice device)
        {
            var interlace = TheConverter.Convert(feature.Value);

            if (interlace != null)
            {
                var desired = interlace.Is(CssKeywords.Interlace);
                var available = device.IsInterlaced;
                return desired == available;
            }

            return false;
        }

        #endregion
    }
}
