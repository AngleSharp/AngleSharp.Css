namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Represents a CSS style rule.
    /// </summary>
    [DebuggerDisplay(null, Name = "CssStyleRule ({SelectorText})")]
    sealed class CssStyleRule : CssRule, ICssStyleRule, ISelectorVisitor
    {
        #region Fields

        private readonly CssStyleDeclaration _style;
        private readonly CssRuleList _rules;
        private ISelector _selector;
        private IEnumerable<ISelector> _selectorList;

        #endregion

        #region ctor

        internal CssStyleRule(ICssStyleSheet owner)
            : base(owner, CssRuleType.Style)
        {
            _style = new CssStyleDeclaration(this);
            _rules = new CssRuleList();
            _selectorList = null;
        }

        #endregion

        #region Properties

        public ISelector Selector
        {
            get => _selector;
            private set => ChangeSelector(value);
        }

        public String SelectorText
        {
            get => _selector?.Text;
            set => ChangeSelector(ParseSelector(value));
        }

        ICssStyleDeclaration ICssStyleRule.Style => _style;

        public CssStyleDeclaration Style => _style;

        public ICssRuleList Rules => _rules;

        #endregion

        #region Methods

        public void Add(ICssRule rule)
        {
            _rules.Add(rule);
            rule.SetParent(this);
        }

        public void Remove(ICssRule rule)
        {
            _rules.Remove(rule);
            rule.SetParent(null);
        }

        internal void SetInvalidSelector(String selectorText)
        {
            _selector = new InvalidSelector(selectorText);
        }

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = (ICssStyleRule)rule;
            ChangeSelector(newRule.Selector);
            _style.SetDeclarations(newRule.Style);
        }

        public Boolean TryMatch(IElement element, IElement? scope, out Priority specificity)
        {
            if (_selectorList is not null)
            {
                foreach (var selector in _selectorList.OrderByDescending(m => m.Specificity))
                {
                    if (selector.Match(element, scope))
                    {
                        specificity = selector.Specificity;
                        return true;
                    }
                }
            }

            if (_selector is not null && _selector.Match(element, scope))
            {
                specificity = _selector.Specificity;
                return true;
            }

            specificity = default;
            return false;
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var block = _style.ToCssBlock(formatter);
            writer.Write(formatter.Rule(SelectorText, null, block));
        }

        #endregion

        #region Selector

        private void ChangeSelector(ISelector value)
        {
            _selectorList = null;
            _selector = value;
            value?.Accept(this);
        }

        void ISelectorVisitor.Attribute(string name, string op, string value)
        {
        }

        void ISelectorVisitor.Type(string name)
        {
        }

        void ISelectorVisitor.Id(string value)
        {
        }

        void ISelectorVisitor.Child(string name, int step, int offset, ISelector selector)
        {
        }

        void ISelectorVisitor.Class(string name)
        {
        }

        void ISelectorVisitor.PseudoClass(string name)
        {
        }

        void ISelectorVisitor.PseudoElement(string name)
        {
        }

        void ISelectorVisitor.List(IEnumerable<ISelector> selectors)
        {
            _selectorList = selectors;
        }

        void ISelectorVisitor.Combinator(IEnumerable<ISelector> selectors, IEnumerable<string> symbols)
        {
        }

        void ISelectorVisitor.Many(IEnumerable<ISelector> selectors)
        {
        }

        sealed class InvalidSelector : ISelector
        {
            private readonly String _text;

            public InvalidSelector(String text)
            {
                _text = text;
            }

            public String Text => _text;

            public Priority Specificity => Priority.Zero;

            public void Accept(ISelectorVisitor visitor)
            {
            }

            public Boolean Match(IElement element, IElement scope) => false;
        }

        #endregion
    }
}
