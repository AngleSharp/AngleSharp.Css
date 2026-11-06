#nullable disable
namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.Parser;
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using AngleSharp.Text;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a CSS Stylesheet.
    /// </summary>
    [DomName("CSSStyleSheet")]
    public sealed class CssStyleSheet : ICssStyleSheet
    {
        #region Fields

        private readonly IBrowsingContext _context;
        private TextSource _source;
        private readonly MediaList _media;
        private readonly CssRuleList _rules;
        private readonly IDocument _ownerDocument;
        private readonly Url _baseUrl;
        private ICssStyleSheet _parent;
        private ICssRule _owner;
        private IElement _element;

        #endregion

        #region ctor

        internal CssStyleSheet(IBrowsingContext context, TextSource source)
        {
            _context = context;
            _source = source;
            _media = new MediaList(context);
            _rules = new CssRuleList();
        }

        /// <summary>
        /// Creates a constructed stylesheet for the current window.
        /// </summary>
        /// <param name="window">The window that creates the stylesheet.</param>
        /// <param name="baseURL">The base URL for relative URLs in the stylesheet.</param>
        /// <param name="media">The media queries for the stylesheet.</param>
        /// <param name="disabled">Whether the stylesheet is disabled.</param>
        [DomConstructor]
        [DomInitDict(offset: 1, optional: true)]
        public CssStyleSheet(IWindow window, String baseURL = null, String media = null, Boolean disabled = false)
            : this(window.Document.Context, new TextSource(String.Empty), true, window.Document, ResolveBaseUrl(window.Document, baseURL))
        {
            IsDisabled = disabled;

            if (!String.IsNullOrEmpty(media))
            {
                _media.SetMediaText(media, throwOnError: true);
            }
        }

        internal CssStyleSheet(IBrowsingContext context, TextSource source, Boolean isConstructed, IDocument ownerDocument, Url baseUrl)
            : this(context, source)
        {
            IsConstructed = isConstructed;
            _ownerDocument = ownerDocument;
            _baseUrl = baseUrl;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the MIME type of the stylesheet.
        /// </summary>
        public String Type => MimeTypeNames.Css;

        /// <summary>
        /// Gets the title of the stylesheet, if it has an owner node.
        /// </summary>
        public String Title => OwnerNode?.GetAttribute(AttributeNames.Title);

        /// <summary>
        /// Gets the media queries associated with the stylesheet.
        /// </summary>
        public IMediaList Media => _media;

        /// <summary>
        /// Gets the rules in the stylesheet.
        /// </summary>
        public ICssRuleList Rules => _rules;

        /// <summary>
        /// Gets the browsing context associated with the stylesheet.
        /// </summary>
        public IBrowsingContext Context => _context;

        /// <summary>
        /// Gets the source that was used to parse the stylesheet.
        /// </summary>
        public TextSource Source => _source;

        /// <summary>
        /// Gets or sets whether the stylesheet is disabled.
        /// </summary>
        public Boolean IsDisabled
        {
            get;
            set;
        }

        internal Boolean IsConstructed { get; }

        internal IDocument OwnerDocument => _ownerDocument;

        internal Url BaseUrl => _baseUrl;

        /// <summary>
        /// Gets the element that owns the stylesheet, if any.
        /// </summary>
        public IElement OwnerNode => _element;

        /// <summary>
        /// Gets the parent stylesheet, if any.
        /// </summary>
        public ICssStyleSheet Parent => _parent;

        /// <summary>
        /// Gets the rule that imports the stylesheet, if any.
        /// </summary>
        public ICssRule OwnerRule => _owner;

        /// <summary>
        /// Gets or sets the URL of the stylesheet.
        /// </summary>
        public String Href
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Writes the stylesheet using the specified formatter.
        /// </summary>
        /// <param name="writer">The destination writer.</param>
        /// <param name="formatter">The formatter to use.</param>
        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write(formatter.Sheet(_rules.GetFormattables()));
        }

        internal void Add(ICssRule rule)
        {
            _rules.Add(rule);
            rule.SetOwner(this);
        }

        internal void Remove(ICssRule rule)
        {
            _rules.Remove(rule);
            rule.SetOwner(null);
        }

        /// <summary>
        /// Removes a rule at the specified index.
        /// </summary>
        /// <param name="index">The index of the rule to remove.</param>
        public void RemoveAt(Int32 index)
        {
            if (index >= 0 && index < _rules.Length)
            {
                var rule = _rules[index];
                Remove(rule);
            }
        }

        /// <summary>
        /// Inserts a rule at the specified index.
        /// </summary>
        /// <param name="ruleText">The text of the rule to insert.</param>
        /// <param name="index">The index at which to insert the rule.</param>
        /// <returns>The index at which the rule was inserted.</returns>
        public Int32 Insert(String ruleText, Int32 index)
        {
            var parser = _context.GetService<ICssParser>();
            var rule = parser.ParseRule(this, ruleText);

            if (IsConstructed && rule is ICssImportRule)
            {
                throw new DomException("SyntaxError");
            }

            _rules.Insert(index, rule);
            rule.SetOwner(this);
            return index;            
        }

        /// <summary>
        /// Sets the importing rule that owns the stylesheet.
        /// </summary>
        /// <param name="rule">The owner rule.</param>
        public void SetOwner(ICssRule rule)
        {
            _owner = rule;
            _parent = rule?.Owner;
            _element = _parent?.OwnerNode;
        }

        /// <summary>
        /// Sets the parent stylesheet.
        /// </summary>
        /// <param name="parent">The parent stylesheet.</param>
        public void SetParent(ICssStyleSheet parent)
        {
            _owner = null;
            _parent = parent;
            _element = parent?.OwnerNode;
        }

        /// <summary>
        /// Sets the owner element of the stylesheet.
        /// </summary>
        /// <param name="element">The owner element.</param>
        public void SetOwner(IElement element)
        {
            _owner = null;
            _parent = null;
            _element = element;
        }

        /// <summary>
        /// Finds a namespace URI by its prefix.
        /// </summary>
        /// <param name="prefix">The prefix to find.</param>
        /// <returns>The namespace URI, if found.</returns>
        public String LocateNamespace(String prefix)
        {
            if (!IsDisabled)
            {
                foreach (var rule in _rules.OfType<ICssNamespaceRule>())
                {
                    if (rule.Prefix.Is(prefix))
                    {
                        return rule.NamespaceUri;
                    }
                }
            }

            return null;
        }

        internal void ReplaceSync(String text)
        {
            if (!IsConstructed)
            {
                throw new DomException("NotAllowedError");
            }

            var parser = _context.GetService<ICssParser>() as CssParser ??
                throw new InvalidOperationException("The browsing context does not provide a CSS parser.");
            var source = new TextSource(text ?? String.Empty);
            var parsed = new CssStyleSheet(_context, source, true, _ownerDocument, _baseUrl);
            parser.ParseStylesheet(source, parsed);
            ReplaceRulesFrom(parsed);
        }

        internal Task<ICssStyleSheet> Replace(String text)
        {
            if (!IsConstructed)
            {
                return Task.FromException<ICssStyleSheet>(new DomException("NotAllowedError"));
            }

            ReplaceSync(text);
            return Task.FromResult<ICssStyleSheet>(this);
        }

        private void ReplaceRulesFrom(CssStyleSheet parsed)
        {
            foreach (var oldRule in _rules.GetFormattables().OfType<ICssRule>())
            {
                oldRule.SetOwner(null);
            }

            _rules.Clear();
            _source = parsed.Source;

            foreach (var rule in parsed._rules.GetFormattables().OfType<ICssRule>())
            {
                if (rule is not ICssImportRule)
                {
                    Add(rule);
                }
            }
        }

        private static Url ResolveBaseUrl(IDocument document, String baseURL)
        {
            if (String.IsNullOrEmpty(baseURL))
            {
                return document.BaseUrl;
            }

            return document.BaseUrl is Url documentBase
                ? new Url(documentBase, baseURL)
                : new Url(baseURL);
        }

        #endregion
    }
}
