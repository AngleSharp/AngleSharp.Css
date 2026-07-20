namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
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
        private Dictionary<Int32, List<ICssComment>>? _comments;

        #endregion

        #region ctor

        internal CssRuleList()
        {
            _rules = new List<ICssRule>();
        }

        #endregion

        #region Index

        public ICssRule this[Int32 index] => _rules[index];

        #endregion

        #region Properties

        public Boolean HasDeclarativeRules => _rules.Any(IsDeclarativeRule);

        public Int32 Length => _rules.Count;

        #endregion

        #region Methods

        public void Clear()
        {
            _rules.Clear();
            _comments?.Clear();
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
                    ShiftCommentsAfterRemove(index);
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

            if (index == Length)
            {
                _rules.Add(rule);
            }
            else
            {
                _rules.Insert(index, rule);
            }

            ShiftCommentsAfterInsert(index);
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
            if (rules is null)
            {
                return;
            }

            var oldLength = _rules.Count;
            _rules.AddRange(rules);
        }

        internal void AddCommentBefore(Int32 index, ICssComment comment)
        {
            _comments ??= new Dictionary<Int32, List<ICssComment>>();

            if (!_comments.TryGetValue(index, out var list))
            {
                list = new List<ICssComment>();
                _comments[index] = list;
            }

            list.Add(comment);
        }

        internal IEnumerable<IStyleFormattable> GetFormattables(Boolean includeComments)
        {
            if (!includeComments || _comments is null || _comments.Count == 0)
            {
                return _rules;
            }

            return EnumerateFormattables();
        }

        #endregion

        #region Implemented Interface

        public IEnumerator<ICssRule> GetEnumerator() => _rules.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        #region Helper

        private IEnumerable<IStyleFormattable> EnumerateFormattables()
        {
            var commentsMap = _comments;

            if (commentsMap is null)
            {
                yield break;
            }

            for (var i = 0; i <= _rules.Count; i++)
            {
                if (commentsMap.TryGetValue(i, out var comments))
                {
                    foreach (var comment in comments)
                    {
                        yield return comment;
                    }
                }

                if (i < _rules.Count)
                {
                    yield return _rules[i];
                }
            }
        }

        private void ShiftCommentsAfterInsert(Int32 index, Int32 step = 1)
        {
            if (_comments is null || _comments.Count == 0 || step <= 0)
            {
                return;
            }

            var shifted = new Dictionary<Int32, List<ICssComment>>();

            foreach (var item in _comments)
            {
                var key = item.Key >= index ? item.Key + step : item.Key;

                if (!shifted.TryGetValue(key, out var list))
                {
                    list = new List<ICssComment>();
                    shifted[key] = list;
                }

                list.AddRange(item.Value);
            }

            _comments = shifted;
        }

        private void ShiftCommentsAfterRemove(Int32 index)
        {
            if (_comments is null || _comments.Count == 0)
            {
                return;
            }

            var shifted = new Dictionary<Int32, List<ICssComment>>();

            foreach (var item in _comments)
            {
                var key = item.Key;

                if (key == index || key == index + 1)
                {
                    key = index;
                }
                else if (key > index + 1)
                {
                    key -= 1;
                }

                if (!shifted.TryGetValue(key, out var list))
                {
                    list = new List<ICssComment>();
                    shifted[key] = list;
                }

                list.AddRange(item.Value);
            }

            _comments = shifted;
        }

        private static Boolean IsDeclarativeRule(ICssRule rule)
        {
            var type = rule.Type;
            return type != CssRuleType.Import && type != CssRuleType.Charset && type != CssRuleType.Namespace;
        }

        #endregion
    }
}
