namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// CSS extensions for the browsing context.
    /// </summary>
    public static class BrowsingContextExtensions
    {
        /// <summary>
        /// Loads a stylesheet resource via its URL.
        /// </summary>
        /// <param name="context">The context to use.</param>
        /// <param name="address">The address of the resource.</param>
        /// <param name="element">The hosting element.</param>
        /// <returns>The async task.</returns>
        public static Task<IStyleSheet> OpenStyleSheetAsync(this IBrowsingContext context, Url address, IElement element)
        {
            return context.OpenStyleSheetAsync(address, element, CancellationToken.None);
        }

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

        internal static DeclarationInfo GetDeclarationInfo(this IBrowsingContext context, String propertyName)
        {
            var factory = context.GetFactory<IDeclarationFactory>();
            return factory.Create(propertyName);
        }

        internal static CssProperty CreateProperty(this IBrowsingContext context, String propertyName)
        {
            var info = context.GetDeclarationInfo(propertyName);
            var provider = context.GetProvider<CssParser>();

            if (info.Flags != PropertyFlags.Unknown || context.IsAllowingUnknownDeclarations())
            {
                return new CssProperty(propertyName, info.Converter, info.Flags);
            }

            return null;
        }

        private static Boolean IsAllowingUnknownDeclarations(this IBrowsingContext context)
        {
            var parser = context.GetProvider<CssParser>();
            return parser != null ? parser.Options.IsIncludingUnknownDeclarations : true;
        }
    }
}
