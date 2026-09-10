#nullable enable
namespace AngleSharp
{
    using AngleSharp.Css;
    using AngleSharp.Css.Parser;
    using System;
    using System.Linq;

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
        public static IConfiguration WithCss(this IConfiguration configuration, CssParserOptions options = default)
        {
            configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));            
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

            // Wraps whatever pseudo-class selector factory is registered so that pseudo-classes
            // it recognizes (e.g. :hover, :active) can be forced per element via
            // ElementExtensions.SetPseudoClass, regardless of which factory instance is in use.
            var pseudoClassFactory = configuration.Services.OfType<IPseudoClassSelectorFactory>().FirstOrDefault();

            if (pseudoClassFactory is not null && pseudoClassFactory is not ForcingPseudoClassSelectorFactory)
            {
                configuration = configuration.WithOnly<IPseudoClassSelectorFactory>(new ForcingPseudoClassSelectorFactory(pseudoClassFactory));
            }

            return configuration
                .WithOnly(Factory.Observer)
                .WithOnly<IStylingService>(service);
        }

        /// <summary>
        /// Registers the render device for the given configuration.
        /// </summary>
        /// <param name="configuration">The configuration to extend.</param>
        /// <param name="renderDevice">The custom device to register, if any.</param>
        /// <returns>The new instance with the render device.</returns>
        public static IConfiguration WithRenderDevice(this IConfiguration configuration, IRenderDevice? renderDevice = null) =>
            configuration.WithOnly<IRenderDevice>(renderDevice ?? new DefaultRenderDevice());
    }
}
