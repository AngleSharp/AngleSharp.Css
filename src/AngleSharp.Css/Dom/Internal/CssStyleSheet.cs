namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Parser;
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using AngleSharp.Text;
    using System;
    using System.IO;

    /// <summary>
    /// Represents a CSS Stylesheet.
    /// </summary>
    sealed class CssStyleSheet : ICssStyleSheet
    {
        #region Fields

        private readonly IBrowsingContext _context;
        private readonly TextSource _source;
        private readonly MediaList _media;
        private readonly CssRuleList _rules;

        #endregion

        #region ctor

        internal CssStyleSheet(IBrowsingContext context, TextSource source)
        {
            _context = context;
            _source = source;
            _media = new MediaList(context);
            _rules = new CssRuleList();
        }

        #endregion

        #region Properties

        public String Type
        {
            get { return MimeTypeNames.Css; }
        }

        public String Title
        {
            get { return OwnerNode?.GetAttribute(AttributeNames.Title); }
        }

        public IMediaList Media
        {
            get { return _media; }
        }

        public ICssRuleList Rules
        {
            get { return _rules; }
        }

        public IBrowsingContext Context
        {
            get { return _context; }
        }

        public TextSource Source
        {
            get { return _source; }
        }

        public Boolean IsDisabled
        {
            get;
            set;
        }

        public IElement OwnerNode
        {
            get;
            set;
        }

        public ICssStyleSheet Parent
        {
            get;
            set;
        }

        public ICssRule OwnerRule
        {
            get;
            set;
        }

        public String Href
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write(formatter.Sheet(Rules));
        }

        public void Add(ICssRule rule)
        {
            _rules.Add(rule);
            rule.SetOwner(this);
        }

        public void Remove(ICssRule rule)
        {
            _rules.Remove(rule);
            rule.SetOwner(null);
        }

        public void RemoveAt(Int32 index)
        {
            if (index >= 0 && index < _rules.Length)
            {
                var rule = _rules[index];
                Remove(rule);
            }
        }

        public Int32 Insert(String ruleText, Int32 index)
        {
            var parser = _context.GetService<ICssParser>();
            var rule = parser.ParseRule(this, ruleText);
            _rules.Insert(index, rule);
            rule.SetOwner(this);
            return index;            
        }

        public void SetOwner(ICssRule rule)
        {
            OwnerRule = rule;
            Parent = rule?.Owner;
        }

        public void SetParent(ICssStyleSheet parent)
        {
            Parent = parent;
        }

        public void SetOwner(IElement element)
        {
            OwnerNode = element;
        }

        #endregion
    }
}
