namespace AngleSharp.Css
{
    using AngleSharp.Dom;
    using AngleSharp.Io;
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
                    var options = new StyleOptions(context) { Element = element };
                    return await service.ParseStylesheetAsync(response, options, cancel).ConfigureAwait(false);
                }
            }

            return null;
        }
    }
}
