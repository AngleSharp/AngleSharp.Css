namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Contains the rules specified by a @document { /* ... */ } rule.
    /// </summary>
    sealed class CssDocumentRule : CssGroupingRule, ICssDocumentRule
    {
        #region Fields

        private readonly DocumentFunctions _conditions;

        #endregion

        #region ctor

        internal CssDocumentRule(ICssStyleSheet owner)
            : base(owner, CssRuleType.Document)
        {
            _conditions = new DocumentFunctions();
        }

        #endregion

        #region Properties

        public String ConditionText
        {
            get
            {
                var entries = Conditions.Select(m => m.ToCss());
                return String.Join(", ", entries); 
            }
            set
            {
                var conditions = Parser.ParseDocumentFunctions(value);

                if (conditions == null)
                    throw new DomException(DomError.Syntax);

                _conditions.Clear();
                _conditions.AddRange(conditions);
            }
        }

        public IDocumentFunctions Conditions
        {
            get { return _conditions; }
        }

        #endregion

        #region Methods

        public void Add(IDocumentFunction condition)
        {
            _conditions.Add(condition);
        }

        public void Remove(IDocumentFunction condition)
        {
            _conditions.Remove(condition);
        }

        protected override void ReplaceWith(ICssRule rule)
        {
            base.ReplaceWith(rule);
            var newRule = (ICssDocumentRule)rule;
            _conditions.Clear();
            _conditions.AddRange(newRule.Conditions);
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var rules = formatter.Block(Rules);
            writer.Write(formatter.Rule("@document", ConditionText, rules));
        }

        #endregion
    }
}
