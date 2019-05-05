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
        /// <param name="options">Optional options for the parser.</param>
        /// <returns>The new instance with the service.</returns>
        public static IConfiguration WithCss(this IConfiguration configuration, CssParserOptions options = default(CssParserOptions))
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            
            var service = new CssStylingService();

            if (!configuration.Has<ICssDefaultStyleSheetProvider>())
            {
                configuration = configuration.With(new CssDefaultStyleSheetProvider());
            }

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

            if (!configuration.Has<IDeclarationFactory>())
            {
                configuration = configuration.With(Factory.Declaration);
            }

            if (!configuration.Has<ICssParser>())
            {
                configuration = configuration.With<ICssParser>(context => new CssParser(options, context));
            }

            return configuration
                .WithOnly(Factory.Observer)
                .WithOnly<IStylingService>(service);
        }
    }
}
