namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Dom.Events;
    using AngleSharp.Css.Parser.Tokens;
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Creates an instance of the CSS parser front-end.
    /// </summary>
    public class CssParser : EventTarget, ICssParser
    {
        #region Fields

        private readonly CssParserOptions _options;
        private readonly IBrowsingContext _context;

        #endregion

        #region Events

        /// <summary>
        /// Adds or removes the event emitted when parsing starts.
        /// </summary>
        public event DomEventHandler Parsing
        {
            add { AddEventListener(EventNames.Parsing, value); }
            remove { RemoveEventListener(EventNames.Parsing, value); }
        }

        /// <summary>
        /// Adds or removes the event emitted when parsing finished.
        /// </summary>
        public event DomEventHandler Parsed
        {
            add { AddEventListener(EventNames.Parsed, value); }
            remove { RemoveEventListener(EventNames.Parsed, value); }
        }

        /// <summary>
        /// Adds or removes the event emitted when a parsing error occurs.
        /// </summary>
        public event DomEventHandler Error
        {
            add { AddEventListener(EventNames.Error, value); }
            remove { RemoveEventListener(EventNames.Error, value); }
        }

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new parser with the default options and context.
        /// </summary>
        public CssParser()
            : this(default(IBrowsingContext))
        {
        }

        /// <summary>
        /// Creates a new parser with the custom options.
        /// </summary>
        /// <param name="options">The options to use.</param>
        public CssParser(CssParserOptions options)
            : this(options, default(IBrowsingContext))
        {
        }

        /// <summary>
        /// Creates a new parser with the context.
        /// </summary>
        /// <param name="context">The context to use.</param>
        internal CssParser(IBrowsingContext context)
            : this(default(CssParserOptions), context)
        {
        }

        /// <summary>
        /// Creates a new parser with the custom options and context.
        /// </summary>
        /// <param name="options">The options to use.</param>
        /// <param name="context">The context to use.</param>
        public CssParser(CssParserOptions options, IBrowsingContext context)
        {
            _options = options;
            _context = context ?? BrowsingContext.New(Configuration.Default.WithOnly<ICssParser>(this).WithCss());
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the specified options.
        /// </summary>
        public CssParserOptions Options
        {
            get { return _options; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Takes a string and transforms it to a stylesheet.
        /// </summary>
        public ICssStyleSheet ParseStyleSheet(String content)
        {
            var source = new TextSource(content);
            return ParseStylesheet(source);
        }

        /// <summary>
        /// Takes a stream and transforms it to a stylesheet.
        /// </summary>
        public ICssStyleSheet ParseStyleSheet(Stream content)
        {
            var source = new TextSource(content);
            return ParseStylesheet(source);
        }

        /// <summary>
        /// Takes a string and transforms it to a stylesheet.
        /// </summary>
        public async Task<ICssStyleSheet> ParseStyleSheetAsync(String content, CancellationToken cancelToken)
        {
            var source = new TextSource(content);
            await source.PrefetchAllAsync(cancelToken).ConfigureAwait(false);
            return ParseStylesheet(source);
        }

        /// <summary>
        /// Takes a stream and transforms it to a stylesheet.
        /// </summary>
        public async Task<ICssStyleSheet> ParseStyleSheetAsync(Stream content, CancellationToken cancelToken)
        {
            var source = new TextSource(content);
            await source.PrefetchAllAsync(cancelToken).ConfigureAwait(false);
            return ParseStylesheet(source);
        }

        /// <summary>
        /// Takes a stylesheet and adds the style rule after parsing its raw text.
        /// </summary>
        public ICssRule ParseRule(ICssStyleSheet owner, String ruleText)
        {
            return Parse(ruleText, (b, t) => b.CreateRule(owner, t));
        }

        /// <summary>
        /// Takes a stylesheet and adds the keyframe rule after parsing its raw text.
        /// </summary>
        public ICssKeyframeRule ParseKeyframeRule(ICssStyleSheet owner, String ruleText)
        {
            var rule = new CssKeyframeRule(owner);
            return Parse(ruleText, (b, t) => b.CreateKeyframeRule(rule, t));
        }

        /// <summary>
        /// Parses the declaration text.
        /// </summary>
        public ICssStyleDeclaration ParseDeclaration(String declarationText)
        {
            var style = new CssStyleDeclaration(_context);
            return Parse(declarationText, (b, t) => b.FillDeclarations(style, t));
        }

        /// <summary>
        /// Takes a blank sheet and transforms the raw content to rules.
        /// </summary>
        public Task<ICssStyleSheet> ParseStyleSheetAsync(ICssStyleSheet sheet, CancellationToken cancelToken)
        {
            return ParseStylesheetAsync((CssStyleSheet)sheet, CancellationToken.None);
        }

        #endregion

        #region Internal Methods

        internal ICssProperty ParseProperty(String propertyText)
        {
            var style = new CssStyleDeclaration(_context);
            return Parse(propertyText, (b, t) =>
            {
                b.CreateDeclarationWith(style, ref t);
                return Tuple.Create(style.Declarations.FirstOrDefault(), t);
            });
        }

        internal ICssStyleSheet ParseStylesheet(TextSource source)
        {
            var sheet = new CssStyleSheet(_context, source);
            var tokenizer = CreateTokenizer(source);
            var start = tokenizer.GetCurrentPosition();
            var builder = new CssBuilder(_options, tokenizer, _context);
            InvokeEventListener(new CssParseEvent(sheet, completed: false));
            builder.CreateRules(sheet);
            InvokeEventListener(new CssParseEvent(sheet, completed: true));
            return sheet;
        }

        internal async Task<ICssStyleSheet> ParseStylesheetAsync(CssStyleSheet sheet, CancellationToken cancel)
        {
            var source = sheet.Source;
            await source.PrefetchAllAsync(cancel).ConfigureAwait(false);
            var tokenizer = CreateTokenizer(source);
            var start = tokenizer.GetCurrentPosition();
            var builder = new CssBuilder(_options, tokenizer, _context);
            var context = sheet.Context;
            var loader = context.GetService<IResourceLoader>();
            var baseUrl = sheet.OwnerNode?.BaseUrl ?? context.Active?.BaseUrl;
            
            if (!String.IsNullOrEmpty(sheet.Href))
            {
                baseUrl = baseUrl != null ? new Url(baseUrl, sheet.Href) : Url.Create(sheet.Href);
            }

            var tasks = new List<Task>();
            InvokeEventListener(new CssParseEvent(sheet, completed: false));
            builder.CreateRules(sheet);
            InvokeEventListener(new CssParseEvent(sheet, completed: true));

            foreach (var rule in sheet.Rules)
            {
                if (rule.Type == CssRuleType.Charset)
                    continue;

                if (rule.Type != CssRuleType.Import)
                    break;

                var import = (CssImportRule)rule;
                var url = baseUrl != null ? new Url(baseUrl, import.Href) : Url.Create(import.Href);

                if (!url.IsInvalid && !IsRecursion(sheet, url) && loader != null)
                {
                    var request = sheet.OwnerNode.CreateRequestFor(url);
                    var download = loader.FetchAsync(request);
                    tasks.Add(ParseChildStyleSheetAsync(download, sheet, cancel).ContinueWith(task =>
                    {
                        if (task.IsCompleted && !task.IsFaulted)
                        {
                            import.Sheet = task.Result;
                        }
                    }));
                }
            }

            await Task.WhenAll(tasks).ConfigureAwait(false);
            return sheet;
        }

        #endregion

        #region Helpers

        private async Task<ICssStyleSheet> ParseChildStyleSheetAsync(IDownload download, ICssStyleSheet parent, CancellationToken cancel)
        {
            using (var response = await download.Task.ConfigureAwait(false))
            {
                var url = response.Address?.Href;
                var source = new TextSource(response.Content);
                var sheet = new CssStyleSheet(_context, source)
                {
                    IsDisabled = false,
                    Href = url
                };
                sheet.SetParent(parent);
                return await ParseStylesheetAsync(sheet, cancel).ConfigureAwait(false);
            }
        }

        private T Parse<T>(String source, Func<CssBuilder, CssToken, T> create)
        {
            var tokenizer = CreateTokenizer(source);
            var token = tokenizer.Get();
            var builder = new CssBuilder(_options, tokenizer, _context);
            var rule = create.Invoke(builder, token);
            return tokenizer.Get().Type == CssTokenType.EndOfFile ? rule : default(T);
        }

        private T Parse<T>(String source, Func<CssBuilder, CssToken, Tuple<T, CssToken>> create)
        {
            var tokenizer = CreateTokenizer(source);
            var token = tokenizer.Get();
            var builder = new CssBuilder(_options, tokenizer, _context);
            var pair = create.Invoke(builder, token);
            return pair.Item2.Type == CssTokenType.EndOfFile ? pair.Item1 : default(T);
        }

        private CssTokenizer CreateTokenizer(String sourceCode)
        {
            var source = new TextSource(sourceCode);
            return CreateTokenizer(source);
        }

        private CssTokenizer CreateTokenizer(TextSource source)
        {
            var tokenizer = new CssTokenizer(source);

            if (HasEventListener(EventNames.Error))
            {
                tokenizer.Error += (_, ev) => InvokeEventListener(ev);
            }

            return tokenizer;
        }

        private static Boolean IsRecursion(ICssStyleSheet sheet, Url url)
        {
            var href = url.Href;

            while (sheet != null && !sheet.Href.Is(href))
            {
                sheet = sheet.Parent;
            }

            return sheet != null;
        }

        #endregion
    }
}
