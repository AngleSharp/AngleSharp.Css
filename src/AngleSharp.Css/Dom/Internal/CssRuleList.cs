namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents an array like structure containing CSS rules.
    /// </summary>
    sealed class CssRuleList : ICssRuleList
    {
        #region Fields

        private readonly List<ICssRule> _rules;

        #endregion

        #region ctor

        internal CssRuleList()
        {
            _rules = new List<ICssRule>();
        }

        #endregion

        #region Index

        public ICssRule this[Int32 index]
        {
            get { return _rules[index]; }
        }

        #endregion

        #region Properties

        public Boolean HasDeclarativeRules
        {
            get { return _rules.Any(IsDeclarativeRule); }
        }

        public Int32 Length
        {
            get { return _rules.Count; }
        }

        #endregion

        #region Methods

        public void Clear()
        {
            _rules.Clear();
        }

        public void RemoveAt(Int32 index)
        {
            if (index < 0 || index >= Length)
                throw new DomException(DomError.IndexSizeError);

            var rule = this[index];

            if (rule.Type == CssRuleType.Namespace && HasDeclarativeRules)
                throw new DomException(DomError.InvalidState);

            Remove(rule);
        }

        public void Remove(ICssRule rule)
        {
            if (rule != null)
            {
                _rules.Remove(rule);
            }
        }

        public void Insert(Int32 index, ICssRule rule)
        {
            if (rule == null)
                throw new DomException(DomError.Syntax);

            if (rule.Type == CssRuleType.Charset)
                throw new DomException(DomError.Syntax);

            if (index > Length || index < 0)
                throw new DomException(DomError.IndexSizeError);

            if (rule.Type == CssRuleType.Namespace && HasDeclarativeRules)
                throw new DomException(DomError.InvalidState);

            if (index == Length)
            {
                _rules.Add(rule);
            }
            else
            {
                _rules.Insert(index, rule);
            }
        }

        public void Add(ICssRule rule)
        {
            if (rule != null)
            {
                _rules.Add(rule);
            }
        }

        public void AddRange(IEnumerable<ICssRule> rules)
        {
            _rules.AddRange(rules);
        }

        #endregion

        #region Implemented Interface

        public IEnumerator<ICssRule> GetEnumerator()
        {
            return _rules.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Helper

        private static Boolean IsDeclarativeRule(ICssRule rule)
        {
            var type = rule.Type;
            return type != CssRuleType.Import && type != CssRuleType.Charset && type != CssRuleType.Namespace;
        }

        #endregion
    }
}
