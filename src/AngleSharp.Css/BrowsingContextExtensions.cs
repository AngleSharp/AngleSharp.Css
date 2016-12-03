namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public static class BrowsingContextExtensions
    {
        public static Task<IStyleSheet> OpenStyleSheetAsync(this IBrowsingContext context, Url address, IElement element)
        {
            return context.OpenStyleSheetAsync(address, element, CancellationToken.None);
        }

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

        internal static CssProperty CreateProperty(this IBrowsingContext context, String propertyName)
        {
            var factory = context.GetFactory<IConverterFactory>();
            var converter = factory.Create(propertyName);
            var flags = PropertyFlags.None;
            Map.KnownPropertyFlags.TryGetValue(propertyName, out flags);
            return new CssProperty(propertyName, converter, flags);
        }
    }
}
