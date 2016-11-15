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

            if (loader != null)
            {
                var engine = context.GetCssStyleEngine();
                var request = element.CreateRequestFor(address);
                var download = loader.FetchAsync(request);

                using (var response = await download.Task.ConfigureAwait(false))
                {
                    var options = new StyleOptions(context) { Element = element };
                    return await engine.ParseStylesheetAsync(response, options, cancel).ConfigureAwait(false);
                }
            }

            return null;
        }
    }
}
