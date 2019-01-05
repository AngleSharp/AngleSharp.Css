namespace AngleSharp.Css.FeatureValidators
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    sealed class GridFeatureValidator : IFeatureValidator
    {
        public Boolean Validate(IMediaFeature feature, IRenderDevice device)
        {
            var grid = BinaryConverter.Convert(feature.Value);

            if (grid != null)
            {
                var desired = grid.AsBoolean();
                var available = device.IsGrid;
                return desired == available;
            }

            return false;
        }
    }
}
