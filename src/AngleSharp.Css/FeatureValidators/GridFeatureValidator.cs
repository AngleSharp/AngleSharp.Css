namespace AngleSharp.Css.FeatureValidators
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    sealed class GridFeatureValidator : IFeatureValidator
    {
        public Boolean Validate(IMediaFeature feature, IRenderDevice renderDevice)
        {
            var grid = BinaryConverter.Convert(feature.Value);

            if (grid != null)
            {
                var desired = grid.AsBoolean();
                var available = renderDevice.IsGrid;
                return desired == available;
            }

            return false;
        }
    }
}
