namespace AngleSharp.Css.Parser
{
    using AngleSharp.Browser;
    using AngleSharp.Css.Dom;
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Methods to parse CSS.
    /// </summary>
    public interface ICssParser : IParser
    {
        /// <summary>
        /// Parses a CSS stylesheet from a string.
        /// </summary>
        /// <param name="content">The string content to parse.</param>
        /// <returns>The created stylesheet.</returns>
        ICssStyleSheet ParseStyleSheet(String content);

        /// <summary>
        /// Parses a CSS stylesheet from a stream of bytes.
        /// </summary>
        /// <param name="content">The stream content to parse.</param>
        /// <returns>The created stylesheet.</returns>
        ICssStyleSheet ParseStyleSheet(Stream content);

        /// <summary>
        /// Parses a CSS rule from a string.
        /// </summary>
        /// <param name="owner">The stylesheet hosting the rule.</param>
        /// <param name="content">The rule snippet to parse.</param>
        /// <returns>The created rule.</returns>
        ICssRule ParseRule(ICssStyleSheet owner, String content);

        /// <summary>
        /// Parses a CSS keyframe rule from a string.
        /// </summary>
        /// <param name="owner">The stylesheet hosting the rule.</param>
        /// <param name="content">The rule snippet to parse.</param>
        /// <returns>The created keyframe rule.</returns>
        ICssKeyframeRule ParseKeyframeRule(ICssStyleSheet owner, String content);

        /// <summary>
        /// Parses a CSS style declaration from a string.
        /// </summary>
        /// <param name="content">The style declaration to parse.</param>
        /// <returns>The created style declaration.</returns>
        ICssStyleDeclaration ParseDeclaration(String content);

        /// <summary>
        /// Parses a CSS stylesheet from a string asynchronously.
        /// </summary>
        /// <param name="content">The string content to parse.</param>
        /// <param name="cancelToken">Token to support cancellation.</param>
        /// <returns>The task yielding the created stylesheet.</returns>
        Task<ICssStyleSheet> ParseStyleSheetAsync(String content, CancellationToken cancelToken);

        /// <summary>
        /// Parses a CSS stylesheet from a string asynchronously.
        /// </summary>
        /// <param name="content">The stream content to parse asynchronously.</param>
        /// <param name="cancelToken">Token to support cancellation.</param>
        /// <returns>The task yielding the created stylesheet.</returns>
        Task<ICssStyleSheet> ParseStyleSheetAsync(Stream content, CancellationToken cancelToken);

        /// <summary>
        /// Parses a CSS stylesheet from hosting stylesheet asynchronously.
        /// </summary>
        /// <param name="sheet">The stylesheet containg the URL reference.</param>
        /// <param name="cancelToken">Token to support cancellation.</param>
        /// <returns>The task yielding the finished stylesheet.</returns>
        Task<ICssStyleSheet> ParseStyleSheetAsync(ICssStyleSheet sheet, CancellationToken cancelToken);
    }
}
