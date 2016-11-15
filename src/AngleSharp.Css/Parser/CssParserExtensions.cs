namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Dom;
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public static class CssParserExtensions
    {
        public static Task<ICssStyleSheet> ParseStyleSheetAsync(this ICssParser parser, String content)
        {
            return parser.ParseStyleSheetAsync(content, CancellationToken.None);
        }

        public static Task<ICssStyleSheet> ParseStyleSheetAsync(this ICssParser parser, Stream content)
        {
            return parser.ParseStyleSheetAsync(content, CancellationToken.None);
        }

        public static Task<ICssStyleSheet> ParseStyleSheetAsync(this ICssParser parser, ICssStyleSheet sheet)
        {
            return parser.ParseStyleSheetAsync(sheet, CancellationToken.None);
        }
    }
}
