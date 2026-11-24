#nullable disable
namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// CSS extensions for the browsing context.
    /// </summary>
    public static class BrowsingContextExtensions
    {
        // the services of a context are fixed; resolving them walks all services
        private static readonly ConditionalWeakTable<IBrowsingContext, IDeclarationFactory> DeclarationFactories = new();
        private static readonly ConditionalWeakTable<IBrowsingContext, IAnimatedValueProvider> AnimatedValueProviders = new();

        // most processes never have one, which spares every computed style the lookup
        private static volatile Boolean _anyAnimatedValueProvider;

        /// <summary>
        /// Sets what gives the animated values of the elements of the context's documents (see
        /// <see cref="IAnimatedValueProvider"/>), or removes it with null. The styles computed from
        /// then on include them; styles computed before stay as they are.
        /// </summary>
        /// <param name="context">The context to use.</param>
        /// <param name="provider">The provider, or null.</param>
        public static void SetAnimatedValueProvider(this IBrowsingContext context, IAnimatedValueProvider provider)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            AnimatedValueProviders.Remove(context);

            if (provider is not null)
            {
                _anyAnimatedValueProvider = true;
                AnimatedValueProviders.Add(context, provider);
            }
        }

        internal static IAnimatedValueProvider GetAnimatedValueProvider(this IBrowsingContext context) =>
            _anyAnimatedValueProvider && context is not null && AnimatedValueProviders.TryGetValue(context, out var provider) ? provider : null;

        /// <summary>
        /// Loads a stylesheet resource via its URL.
        /// </summary>
        /// <param name="context">The context to use.</param>
        /// <param name="address">The address of the resource.</param>
        /// <param name="element">The hosting element.</param>
        /// <returns>The async task.</returns>
        public static Task<IStyleSheet> OpenStyleSheetAsync(this IBrowsingContext context, Url address, IElement element) =>
            context.OpenStyleSheetAsync(address, element, CancellationToken.None);

        /// <summary>
        /// Loads a stylesheet resource via its URL.
        /// </summary>
        /// <param name="context">The context to use.</param>
        /// <param name="address">The address of the resource.</param>
        /// <param name="element">The hosting element.</param>
        /// <param name="cancel">The cancellation token.</param>
        /// <returns>The async task.</returns>
        public static async Task<IStyleSheet> OpenStyleSheetAsync(this IBrowsingContext context, Url address, IElement element, CancellationToken cancel)
        {
            var loader = context.GetService<IResourceLoader>();
            var service = context.GetCssStyling();

            if (loader != null && service != null)
            {
                var request = element.CreateRequestFor(address);
                var download = loader.FetchAsync(request);

                using (var response = await download.Task.ConfigureAwait(false))
                {
                    var options = new StyleOptions(element?.Owner ?? context.Active) { Element = element };
                    return await service.ParseStylesheetAsync(response, options, cancel).ConfigureAwait(false);
                }
            }

            return null;
        }

        internal static IDeclarationFactory GetDeclarationFactory(this IBrowsingContext context) =>
            DeclarationFactories.GetValue(context, ctx => ctx.GetFactory<IDeclarationFactory>());

        internal static DeclarationInfo GetDeclarationInfo(this IBrowsingContext context, String propertyName)
        {
            var factory = context.GetDeclarationFactory();
            return factory.Create(propertyName);
        }

        internal static ICssProperty CreateShorthand(this IBrowsingContext context, String name, ICssValue[] longhands, Boolean important)
        {
            var factory = context.GetDeclarationFactory();
            var info = factory.Create(name);
            var value = info.Collapse(factory, longhands);

            if (context.AllowsDeclaration(info))
            {
                return new CssProperty(name, info.Converter, info.Flags, value, important);
            }

            return null;
        }

        internal static ICssProperty[] CreateLonghands(this IBrowsingContext context, ICssProperty shorthand)
        {
            var factory = context.GetDeclarationFactory();
            var info = factory.Create(shorthand.Name);
            var values = info.Expand(factory, shorthand.RawValue);
            return factory.CreateProperties(info.Longhands, values, shorthand.IsImportant);
        }

        internal static CssProperty CreateProperty(this IBrowsingContext context, String propertyName)
        {
            var info = context.GetDeclarationInfo(propertyName);

            if (context.AllowsDeclaration(info))
            {
                return new CssProperty(propertyName, info.Converter, info.Flags);
            }

            return null;
        }

        private static Boolean AllowsDeclaration(this IBrowsingContext context, DeclarationInfo info) =>
            info.Flags != PropertyFlags.Unknown || context.IsAllowingUnknownDeclarations();

        internal static Boolean IsAllowingUnknownDeclarations(this IBrowsingContext context)
        {
            var parser = context.GetProvider<CssParser>();
            return parser?.Options.IsIncludingUnknownDeclarations ?? true;
        }

        private static ICssProperty[] CreateProperties(this IDeclarationFactory factory, String[] names, ICssValue[] values, Boolean important)
        {
            if (values != null && values.Length == names.Length)
            {
                var properties = new ICssProperty[names.Length];

                for (var i = 0; i < names.Length; i++)
                {
                    var info = factory.Create(names[i]);
                    properties[i] = new CssProperty(names[i], info.Converter, info.Flags, values[i], important);
                }

                return properties;
            }

            return null;
        }
    }
}
