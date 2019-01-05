namespace AngleSharp.Css
{
    using AngleSharp.Css.FeatureValidators;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides string to media feature validator creation mappings.
    /// </summary>
    public class DefaultFeatureValidatorFactory : IFeatureValidatorFactory
    {
        /// <summary>
        /// Represents a creator delegate for creating feature validators.
        /// </summary>
        /// <returns>The created media feature validator.</returns>
        public delegate IFeatureValidator Creator();

        private readonly Dictionary<String, Creator> _creators = new Dictionary<String, Creator>(StringComparer.OrdinalIgnoreCase)
        {
            { FeatureNames.MinWidth, () => new WidthFeatureValidator() },
            { FeatureNames.MaxWidth, () => new WidthFeatureValidator() },
            { FeatureNames.Width, () => new WidthFeatureValidator() },
            { FeatureNames.MinHeight, () => new HeightFeatureValidator() },
            { FeatureNames.MaxHeight, () => new HeightFeatureValidator() },
            { FeatureNames.Height, () => new HeightFeatureValidator() },
            { FeatureNames.MinDeviceWidth, () => new DeviceWidthFeatureValidator() },
            { FeatureNames.MaxDeviceWidth, () => new DeviceWidthFeatureValidator() },
            { FeatureNames.DeviceWidth, () => new DeviceWidthFeatureValidator() },
            { FeatureNames.MinDevicePixelRatio, () => new DevicePixelRatioFeatureValidator() },
            { FeatureNames.MaxDevicePixelRatio, () => new DevicePixelRatioFeatureValidator() },
            { FeatureNames.DevicePixelRatio, () => new DevicePixelRatioFeatureValidator() },
            { FeatureNames.MinDeviceHeight, () => new DeviceHeightFeatureValidator() },
            { FeatureNames.MaxDeviceHeight, () => new DeviceHeightFeatureValidator() },
            { FeatureNames.DeviceHeight, () => new DeviceHeightFeatureValidator() },
            { FeatureNames.MinAspectRatio, () => new AspectRatioFeatureValidator() },
            { FeatureNames.MaxAspectRatio, () => new AspectRatioFeatureValidator() },
            { FeatureNames.AspectRatio, () => new AspectRatioFeatureValidator() },
            { FeatureNames.MinDeviceAspectRatio, () => new DeviceAspectRatioFeatureValidator() },
            { FeatureNames.MaxDeviceAspectRatio, () => new DeviceAspectRatioFeatureValidator() },
            { FeatureNames.DeviceAspectRatio, () => new DeviceAspectRatioFeatureValidator() },
            { FeatureNames.MinColor, () => new ColorFeatureValidator() },
            { FeatureNames.MaxColor, () => new ColorFeatureValidator() },
            { FeatureNames.Color, () => new ColorFeatureValidator() },
            { FeatureNames.MinColorIndex, () => new ColorIndexFeatureValidator() },
            { FeatureNames.MaxColorIndex, () => new ColorIndexFeatureValidator() },
            { FeatureNames.ColorIndex, () => new ColorIndexFeatureValidator() },
            { FeatureNames.MinMonochrome, () => new MonochromeFeatureValidator() },
            { FeatureNames.MaxMonochrome, () => new MonochromeFeatureValidator() },
            { FeatureNames.Monochrome, () => new MonochromeFeatureValidator() },
            { FeatureNames.MinResolution, () => new ResolutionFeatureValidator() },
            { FeatureNames.MaxResolution, () => new ResolutionFeatureValidator() },
            { FeatureNames.Resolution, () => new ResolutionFeatureValidator() },
            { FeatureNames.Orientation, () => new OrientationFeatureValidator() },
            { FeatureNames.Grid, () => new GridFeatureValidator() },
            { FeatureNames.Scan, () => new ScanFeatureValidator() },
            { FeatureNames.UpdateFrequency, () => new UpdateFrequencyFeatureValidator() },
            { FeatureNames.Scripting, () => new ScanFeatureValidator() },
            { FeatureNames.Pointer, () => new PointerFeatureValidator() },
            { FeatureNames.Hover, () => new HoverFeatureValidator() }
        };

        /// <summary>
        /// Registers a new validator for the specified feature.
        /// Throws an exception if another validator for the given
        /// feature name is already added.
        /// </summary>
        /// <param name="name">The name of the feature.</param>
        /// <param name="creator">The creator to invoke.</param>
        public void Register(String name, Creator creator)
        {
            _creators.Add(name, creator);
        }

        /// <summary>
        /// Unregisters an existing validator for the given feature.
        /// </summary>
        /// <param name="name">The name of the feature.</param>
        /// <returns>The registered creator, if any.</returns>
        public Creator Unregister(String name)
        {
            var creator = default(Creator);

            if (_creators.TryGetValue(name, out creator))
            {
                _creators.Remove(name);
            }

            return creator;
        }

        /// <summary>
        /// Creates the default media feature validator for the given
        /// media feature name. Returns null by default.
        /// </summary>
        /// <param name="name">The name of the feature.</param>
        /// <returns>The feature validator.</returns>
        protected virtual IFeatureValidator CreateDefault(String name)
        {
            return default(IFeatureValidator);
        }

        /// <summary>
        /// Creates a new media feature validator.
        /// </summary>
        /// <param name="name">The name of the feature.</param>
        /// <returns>The created feature validator.</returns>
        public IFeatureValidator Create(String name)
        {
            var creator = default(Creator);

            if (_creators.TryGetValue(name, out creator))
            {
                return creator();
            }

            return CreateDefault(name);
        }
    }
}
