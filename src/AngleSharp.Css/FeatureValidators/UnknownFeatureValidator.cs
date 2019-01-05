namespace AngleSharp.Css.FeatureValidators
{
    using AngleSharp.Css.Dom;
    using System;

    sealed class UnknownFeatureValidator : IFeatureValidator
    {
        public Boolean Validate(IMediaFeature feature, IRenderDevice device)
        {
            return true;
        }
    }
}
