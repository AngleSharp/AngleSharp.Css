namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using AngleSharp.Text;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The CSS style engine for creating CSSStyleSheet instances.
    /// </summary>
    public class CssStylingService : IStylingService
    {
        #region Methods

        Boolean IStylingService.SupportsType(String mimeType) => mimeType.Isi(MimeTypeNames.Css);

        /// <summary>
        /// Creates a style sheet for the given response asynchronously.
        /// </summary>
        /// <param name="response">
        /// The response with the stream representing the source of the
        /// stylesheet.
        /// </param>
        /// <param name="options">
        /// The options with the parameters for evaluating the style.
        /// </param>
        /// <param name="cancel">The cancellation token.</param>
        /// <returns>The task resulting in the style sheet.</returns>
        public async Task<IStyleSheet> ParseStylesheetAsync(IResponse response, StyleOptions options, CancellationToken cancel)
        {
            var context = options.Document.Context;
            var parser = context.GetService<ICssParser>();
            var url = response.Address?.Href;
            var source = new TextSource(response.Content);
            var sheet = new CssStyleSheet(context, source)
            {
                IsDisabled = options.IsDisabled,
                Href = url
            };
            sheet.SetOwner(options.Element);
            return await parser.ParseStyleSheetAsync(sheet, cancel).ConfigureAwait(false);
        }

        #endregion
    }
}
