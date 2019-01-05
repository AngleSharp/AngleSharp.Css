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
        #region Fields

        private ICssStyleSheet _default;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the default stylesheet as specified by the W3C:
        /// http://www.w3.org/TR/CSS21/sample.html
        /// </summary>
        public ICssStyleSheet Default
        {
            get { return _default ?? (_default = ParseDefault()); }
        }

        #endregion

        #region Methods

        Boolean IStylingService.SupportsType(String mimeType)
        {
            return mimeType.Isi(MimeTypeNames.Css);
        }

        /// <summary>
        /// Sets a new default stylesheet to use.
        /// </summary>
        /// <param name="sheet">The default stylesheet to use.</param>
        public void SetDefault(ICssStyleSheet sheet)
        {
            _default = sheet;
        }

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

        #region Default Stylesheet

        private ICssStyleSheet ParseDefault()
        {
            var parser = new CssParser();
            return parser.ParseStyleSheet(DefaultSource);
        }

        /// <summary>
        /// Gets the source code for the by default used base stylesheet.
        /// </summary>
        public static readonly String DefaultSource = @"
html, address,
blockquote,
body, dd, div,
dl, dt, fieldset, form,
frame, frameset,
h1, h2, h3, h4,
h5, h6, noframes,
ol, p, ul, center,
dir, hr, menu, pre   { display: block; unicode-bidi: embed }
li              { display: list-item }
head            { display: none }
table           { display: table }
tr              { display: table-row }
thead           { display: table-header-group }
tbody           { display: table-row-group }
tfoot           { display: table-footer-group }
col             { display: table-column }
colgroup        { display: table-column-group }
td, th          { display: table-cell }
caption         { display: table-caption }
th              { font-weight: bolder; text-align: center }
caption         { text-align: center }
body            { margin: 8px }
h1              { font-size: 2em; margin: .67em 0 }
h2              { font-size: 1.5em; margin: .75em 0 }
h3              { font-size: 1.17em; margin: .83em 0 }
h4, p,
blockquote, ul,
fieldset, form,
ol, dl, dir,
menu            { margin: 1.12em 0 }
h5              { font-size: .83em; margin: 1.5em 0 }
h6              { font-size: .75em; margin: 1.67em 0 }
h1, h2, h3, h4,
h5, h6, b,
strong          { font-weight: bolder }
blockquote      { margin-left: 40px; margin-right: 40px }
i, cite, em,
var, address    { font-style: italic }
pre, tt, code,
kbd, samp       { font-family: monospace }
pre             { white-space: pre }
button, textarea,
input, select   { display: inline-block }
big             { font-size: 1.17em }
small, sub, sup { font-size: .83em }
sub             { vertical-align: sub }
sup             { vertical-align: super }
table           { border-spacing: 2px; }
thead, tbody,
tfoot           { vertical-align: middle }
td, th, tr      { vertical-align: inherit }
s, strike, del  { text-decoration: line-through }
hr              { border: 1px inset }
ol, ul, dir,
menu, dd        { margin-left: 40px }
ol              { list-style-type: decimal }
ol ul, ul ol,
ul ul, ol ol    { margin-top: 0; margin-bottom: 0 }
u, ins          { text-decoration: underline }
br:before       { content: '\A'; white-space: pre-line }
center          { text-align: center }
:link, :visited { text-decoration: underline }
:focus          { outline: thin dotted invert }

/* Begin bidirectionality settings (do not change) */
BDO[DIR='ltr']  { direction: ltr; unicode-bidi: bidi-override }
BDO[DIR='rtl']  { direction: rtl; unicode-bidi: bidi-override }

*[DIR='ltr']    { direction: ltr; unicode-bidi: embed }
*[DIR='rtl']    { direction: rtl; unicode-bidi: embed }

@media print {
  h1            { page-break-before: always }
  h1, h2, h3,
  h4, h5, h6    { page-break-after: avoid }
  ul, ol, dl    { page-break-before: avoid }
}";

        #endregion
    }
}
