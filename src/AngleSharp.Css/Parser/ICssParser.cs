namespace AngleSharp.Css.Parser
{
    using AngleSharp.Browser;
    using AngleSharp.Css.Dom;
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ICssParser : IParser
    {
        ICssStyleSheet ParseStyleSheet(String content);

        ICssStyleSheet ParseStyleSheet(Stream content);

        ICssRule ParseRule(ICssStyleSheet owner, String content);

        ICssKeyframeRule ParseKeyframeRule(ICssStyleSheet owner, String content);

        ICssStyleDeclaration ParseDeclaration(String content);

        Task<ICssStyleSheet> ParseStyleSheetAsync(String content, CancellationToken cancelToken);

        Task<ICssStyleSheet> ParseStyleSheetAsync(Stream content, CancellationToken cancelToken);

        Task<ICssStyleSheet> ParseStyleSheetAsync(ICssStyleSheet sheet, CancellationToken cancelToken);
    }
}
