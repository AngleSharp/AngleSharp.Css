namespace AngleSharp.Css.FeatureValidators
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    sealed class PointerFeatureValidator : IFeatureValidator
    {
        public Boolean Validate(IMediaFeature feature, IRenderDevice device)
        {
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
