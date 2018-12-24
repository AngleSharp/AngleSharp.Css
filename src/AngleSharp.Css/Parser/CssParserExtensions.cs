namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Dom;
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extensions for CSS parsing methods.
    /// </summary>
    public static class CssParserExtensions
    {
        /// <summary>
        /// Parses a CSS stylesheet from a string asynchronously.
        /// </summary>
        /// <param name="parser">The parser to extend.</param>
        /// <param name="content">The string content to parse.</param>
        /// <returns>The task yielding the created stylesheet.</returns>
        public static Task<ICssStyleSheet> ParseStyleSheetAsync(this ICssParser parser, String content)
        {
            return parser.ParseStyleSheetAsync(content, CancellationToken.None);
        }

        /// <summary>
        /// Parses a CSS stylesheet from a string asynchronously.
        /// </summary>
        /// <param name="parser">The parser to extend.</param>
        /// <param name="content">The stream content to parse asynchronously.</param>
        /// <returns>The task yielding the created stylesheet.</returns>
        public static Task<ICssStyleSheet> ParseStyleSheetAsync(this ICssParser parser, Stream content)
        {
            return parser.ParseStyleSheetAsync(content, CancellationToken.None);
        }

        /// <summary>
        /// Parses a CSS stylesheet from hosting stylesheet asynchronously.
        /// </summary>
        /// <param name="parser">The parser to extend.</param>
        /// <param name="sheet">The stylesheet containg the URL reference.</param>
        /// <returns>The task yielding the finished stylesheet.</returns>
        public static Task<ICssStyleSheet> ParseStyleSheetAsync(this ICssParser parser, ICssStyleSheet sheet)
        {
            return parser.ParseStyleSheetAsync(sheet, CancellationToken.None);
        }
    }
}
