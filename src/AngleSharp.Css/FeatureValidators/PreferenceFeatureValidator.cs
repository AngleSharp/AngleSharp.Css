namespace AngleSharp.Css.FeatureValidators
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Validates a user preference media feature, e.g., prefers-color-scheme,
    /// against the preferences carried by the render device.
    /// https://drafts.csswg.org/mediaqueries-5/#mf-user-preferences
    /// </summary>
    sealed class PreferenceFeatureValidator : IFeatureValidator
    {
        private readonly String _name;
        private readonly String? _noPreference;

        /// <summary>
        /// Creates a validator for the given media feature.
        /// </summary>
        /// <param name="name">The name of the media feature, which is also the key of the preference.</param>
        /// <param name="noPreference">The keyword that evaluates to false in a boolean context, if any.</param>
        public PreferenceFeatureValidator(String name, String? noPreference)
        {
            _name = name;
            _noPreference = noPreference;
        }

        public Boolean Validate(IMediaFeature feature, IRenderDevice renderDevice)
        {
            var preference = renderDevice.GetPreference(_name);
            return preference is not null && Matches(feature, preference, _noPreference);
        }

        /// <summary>
        /// Compares the queried keyword against the preference of the device.
        /// A feature used without a value is evaluated in a boolean context,
        /// where the keyword standing for "no preference" yields false.
        /// https://drafts.csswg.org/mediaqueries-5/#mq-boolean-context
        /// </summary>
        /// <param name="feature">The feature to examine.</param>
        /// <param name="preference">The preference carried by the device.</param>
        /// <param name="noPreference">The keyword that evaluates to false in a boolean context, if any.</param>
        /// <returns>True if the feature is present, otherwise false.</returns>
        public static Boolean Matches(IMediaFeature feature, String preference, String? noPreference)
        {
            if (!feature.HasValue)
            {
                return noPreference is null || !preference.Isi(noPreference);
            }

            return preference.Isi(feature.Value);
        }
    }
}
