namespace AngleSharp.Css.FeatureValidators
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    sealed class ScriptingFeatureValidator : IFeatureValidator
    {
        public Boolean Validate(IMediaFeature feature, IRenderDevice device)
        {
            var state = ScriptingStateConverter.Convert(feature.Value);

            if (state != null)
            {
                var available = ScriptingState.None;
                var desired = state.AsEnum<ScriptingState>();

                if (device.IsScripting)
                {
                    available = device.Category == DeviceCategory.Screen ? ScriptingState.Enabled : ScriptingState.InitialOnly;
                }

                return desired == available;
            }

            return false;
        }
    }
}
