namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using System;
    using System.Collections;
    using System.Collections.Generic;

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

        public ICssRule this[Int32 index] => GetRuleAt(index);

        #endregion

        #region Properties

        public Boolean HasDeclarativeRules
        {
            get
            {
                for (var i = 0; i < _rules.Count; i++)
                {
                    if (IsDeclarativeRule(_rules[i]))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public Int32 Length
        {
            get
            {
                var count = 0;

                for (var i = 0; i < _rules.Count; i++)
                {
                    if (!IsCommentRule(_rules[i]))
                    {
                        count++;
                    }
                }

                return count;
            }
        }

        #endregion

        #region Methods

        public void Clear()
        {
            _rules.Clear();
            CssPropertyRegistryCache.Invalidate();
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
                var index = _rules.IndexOf(rule);

                if (index >= 0)
                {
                    _rules.RemoveAt(index);
                    Invalidate(rule);
                }
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

            var actualIndex = GetActualIndex(index);

            if (actualIndex == _rules.Count)
            {
                _rules.Add(rule);
            }
            else
            {
                _rules.Insert(actualIndex, rule);
            }

            Invalidate(rule);
        }

        public void Add(ICssRule rule)
        {
            if (rule != null)
            {
                _rules.Add(rule);
                Invalidate(rule);
            }
        }

        public void AddRange(IEnumerable<ICssRule> rules)
        {
            if (rules is null)
            {
                return;
            }

            var oldLength = _rules.Count;
            _rules.AddRange(rules);
            CssPropertyRegistryCache.Invalidate();
        }

        /// <summary>
        /// Marks the registered custom properties as outdated, unless the rule cannot hold
        /// a @property rule, i.e., is a style rule (whose nested rules are not registered).
        /// </summary>
        private static void Invalidate(ICssRule rule)
        {
            if (rule is not ICssStyleRule)
            {
                CssPropertyRegistryCache.Invalidate();
            }
        }

        internal IEnumerable<IStyleFormattable> GetFormattables() => _rules;

        #endregion

        #region Implemented Interface

        public IEnumerator<ICssRule> GetEnumerator()
        {
            for (var i = 0; i < _rules.Count; i++)
            {
                var rule = _rules[i];

                if (!IsCommentRule(rule))
                {
                    yield return rule;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        #region Helper

        private static Boolean IsDeclarativeRule(ICssRule rule)
        {
            var type = rule.Type;
            return type != CssRuleType.Import && type != CssRuleType.Charset && type != CssRuleType.Namespace && type != CssRuleType.Comment;
        }

        private static Boolean IsCommentRule(ICssRule rule) => rule.Type == CssRuleType.Comment;

        private ICssRule GetRuleAt(Int32 index)
        {
            if (index < 0)
            {
                throw new DomException(DomError.IndexSizeError);
            }

            var visible = 0;

            for (var i = 0; i < _rules.Count; i++)
            {
                var rule = _rules[i];

                if (IsCommentRule(rule))
                {
                    continue;
                }

                if (visible == index)
                {
                    return rule;
                }

                visible++;
            }

            throw new DomException(DomError.IndexSizeError);
        }

        private Int32 GetActualIndex(Int32 visibleIndex)
        {
            if (visibleIndex < 0)
            {
                throw new DomException(DomError.IndexSizeError);
            }

            var visible = 0;

            for (var i = 0; i < _rules.Count; i++)
            {
                if (IsCommentRule(_rules[i]))
                {
                    continue;
                }

                if (visible == visibleIndex)
                {
                    return i;
                }

                visible++;
            }

            return visibleIndex == visible ? _rules.Count : throw new DomException(DomError.IndexSizeError);
        }

        #endregion
    }
}
