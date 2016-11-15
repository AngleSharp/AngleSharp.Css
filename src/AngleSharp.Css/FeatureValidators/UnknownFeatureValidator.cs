namespace AngleSharp.Css.FeatureValidators
{
    using AngleSharp.Css.Dom;
    using System;

    sealed class UnknownFeatureValidator : IFeatureValidator
    {
        #region Methods

        public Boolean Validate(IMediaFeature feature, IRenderDevice device)
        {
            return true;
        }

        #endregion
    }
}
