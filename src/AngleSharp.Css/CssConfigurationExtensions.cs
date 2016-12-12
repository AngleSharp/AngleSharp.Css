namespace AngleSharp
{
    using AngleSharp.Css;
    using AngleSharp.Css.Parser;
    using System;

    /// <summary>
    /// Extensions for the configuration.
    /// </summary>
    public static class CssConfigurationExtensions
    {
        /// <summary>
        /// Registers the default styling service with a new CSS style engine
        /// to retrieve, if no other styling service has been registered yet.
        /// </summary>
        /// <param name="configuration">The configuration to extend.</param>
        /// <param name="setup">Optional setup for the style engine.</param>
        /// <returns>The new instance with the service.</returns>
        public static IConfiguration WithCss(this IConfiguration configuration, CssParserOptions options = default(CssParserOptions), Action<CssStylingService> setup = null)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            
            var service = new CssStylingService();
            setup?.Invoke(service);

            if (!configuration.Has<IFeatureValidatorFactory>())
            {
                configuration = configuration.With(Factory.FeatureValidator);
            }

            if (!configuration.Has<IDocumentFunctionFactory>())
            {
                configuration = configuration.With(Factory.DocumentFunction);
            }

            if (!configuration.Has<IPseudoElementFactory>())
            {
                configuration = configuration.With(Factory.PseudoElement);
            }

            if (!configuration.Has<IConverterFactory>())
            {
                configuration = configuration.With(Factory.Converter);
            }

            if (!configuration.Has<ICssParser>())
            {
                configuration = configuration.With<ICssParser>(context => new CssParser(options, context));
            }

            return configuration.WithOnly(Factory.Observer).With(service);
        }
    }
}
