namespace AngleSharp.Css
{
    using AngleSharp.Css.Parser;
    using System;

    /// <summary>
    /// Extensions for the configuration.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Registers the default styling service with a new CSS style engine
        /// to retrieve, if no other styling service has been registered yet.
        /// </summary>
        /// <param name="configuration">The configuration to extend.</param>
        /// <param name="setup">Optional setup for the style engine.</param>
        /// <returns>The new instance with the service.</returns>
        public static IConfiguration WithCss(this IConfiguration configuration, CssParserOptions options = default(CssParserOptions), Action<CssStyleEngine> setup = null)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            
            var service = new DefaultStylingProvider();
            var engine = new CssStyleEngine();
            setup?.Invoke(engine);
            service.Register(engine);

            if (!configuration.Has<IFeatureValidatorFactory>())
            {
                configuration = configuration.With(Factory.FeatureValidator);
            }
            
            if (!configuration.Has<IPseudoElementFactory>())
            {
                configuration = configuration.With(Factory.PseudoElement);
            }

            if (!configuration.Has<ICssPropertyFactory>())
            {
                configuration = configuration.With(Factory.Property);
            }

            if (!configuration.Has<ICssParser>())
            {
                configuration = configuration.With<ICssParser>(context => new CssParser(options, context));
            }

            return configuration.Without<IStylingProvider>().With(service);
        }
    }
}
