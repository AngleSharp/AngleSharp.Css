namespace AngleSharp.Css.FeatureValidators
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;

    sealed class ScriptingFeatureValidator : IFeatureValidator
    {
        #region Fields

        private static readonly IValueConverter TheConverter = Map.ScriptingStates.ToConverter();

        #endregion
        
        #region Methods

        public Boolean Validate(IMediaFeature feature, IRenderDevice device)
        {
            var state = TheConverter.Convert(feature.Value);

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

        #endregion
    }
}
