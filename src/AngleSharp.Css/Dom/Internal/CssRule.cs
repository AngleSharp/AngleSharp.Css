namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Parser;
    using AngleSharp.Dom;
    using System;
    using System.IO;

    /// <summary>
    /// Represents a CSS rule.
    /// </summary>
    abstract class CssRule : ICssRule
    {
        #region Fields
        
        private readonly CssRuleType _type;

        private ICssStyleSheet _owner;
        private ICssRule _parent;

        #endregion

        #region ctor

        internal CssRule(ICssStyleSheet owner, CssRuleType type)
        {
            _owner = owner;
            _type = type;
        }

        #endregion

        #region Properties

        public String CssText
        {
            get { return this.ToCss(); }
            set
            {
                var rule = Parser.ParseRule(Owner, value);

                if (rule == null)
                    throw new DomException(DomError.Syntax);

                if (rule.Type != _type)
                    throw new DomException(DomError.InvalidModification);

                ReplaceWith(rule);
            }
        }

        public ICssRule Parent
        {
            get { return _parent; }
            set
            {
                _parent = value;

                if (value != null)
                {
                    _owner = _parent.Owner;
                }
            }
        }

        public ICssStyleSheet Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        public CssRuleType Type
        {
            get { return _type; }
        }

        public ICssParser Parser
        {
            get { return _owner.Context.GetService<ICssParser>(); }
        }

        #endregion

        #region Methods

        public void SetParent(ICssRule rule)
        {
            _parent = rule;
            _owner = rule?.Owner;
        }

        public void SetOwner(ICssStyleSheet sheet)
        {
            _parent = null;
            _owner = sheet;
        }

        public abstract void ToCss(TextWriter writer, IStyleFormatter formatter);

        #endregion

        #region Helpers

        protected abstract void ReplaceWith(ICssRule rule);

        protected ISelector ParseSelector(String selectorText)
        {
            var parser = _owner.Context.GetService<ICssSelectorParser>();
            return parser.ParseSelector(selectorText);
        }

        #endregion
    }
}
