#nullable disable
namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Represents the base class for all style-rule similar rules.
    /// </summary>
    abstract class CssDeclarationRule : CssRule, ICssProperties
    {
        #region Fields

        private readonly List<ICssProperty> _declarations;
        private readonly HashSet<String> _contained;
        private readonly String _name;
        private Dictionary<Int32, List<ICssComment>> _comments;

        #endregion

        #region ctor

        internal CssDeclarationRule(ICssStyleSheet owner, CssRuleType type, String name, HashSet<String> contained)
            : base(owner, type)
        {
            _declarations = new List<ICssProperty>();
            _contained = contained;
            _name = name;
        }

        #endregion

        #region Properties

        public String this[String propertyName] => GetValue(propertyName);

        public Int32 Length => _declarations.Count;

        #endregion

        #region Methods

        public ICssProperty GetProperty(String propertyName) =>
            _declarations.Find(m => m.Name.Is(propertyName));

        public String GetPropertyValue(String propertyName) => GetValue(propertyName);

        public String GetPropertyPriority(String propertyName) => null;

        public void SetProperty(String propertyName, String propertyValue, String priority = null) =>
            SetValue(propertyName, propertyValue);

        public String RemoveProperty(String propertyName)
        {
            for (var i = 0; i < _declarations.Count; i++)
            {
                var declaration = _declarations[i];

                if (declaration.Name.Is(propertyName))
                {
                    _declarations.RemoveAt(i);
                    ShiftCommentsAfterRemove(i);
                    return declaration.Value;
                }
            }

            return null;
        }

        public IEnumerator<ICssProperty> GetEnumerator() => _declarations.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var includeComments = formatter is ICommentPreservingFormatter commentFormatter && commentFormatter.PreserveComments;
            var block = !includeComments || _comments is null || _comments.Count == 0
                ? formatter.BlockDeclarations(_declarations)
                : formatter.BlockDeclarations(GetFormattablesWithComments());
            writer.Write(formatter.Rule(_name, null, block));
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

        #endregion

        #region Helpers

        private ICssProperty CreateNewProperty(String propertyName)
        {
            if (_contained.Contains(propertyName))
            {
                return Owner.Context.CreateProperty(propertyName);
            }

            return null;
        }

        protected String GetValue(String propertyName)
        {
            foreach (var declaration in _declarations)
            {
                if (declaration.Name.Is(propertyName))
                {
                    return declaration.Value;
                }
            }

            return String.Empty;
        }

        protected void SetValue(String propertyName, String valueText)
        {
            if (!String.IsNullOrEmpty(valueText))
            {
                foreach (var declaration in _declarations)
                {
                    if (declaration.Name.Is(propertyName))
                    {
                        declaration.Value = valueText;
                        return;
                    }
                }

                var property = CreateNewProperty(propertyName);

                if (property != null)
                {
                    property.Value = valueText;
                    _declarations.Add(property);
                }
            }
            else
            {
                RemoveProperty(propertyName);
            }
        }

        #endregion

        #region Comment Helpers

        private IEnumerable<IStyleFormattable> GetFormattablesWithComments()
        {
            for (var i = 0; i <= _declarations.Count; i++)
            {
                if (_comments.TryGetValue(i, out var comments))
                {
                    foreach (var comment in comments)
                    {
                        yield return comment;
                    }
                }

                if (i < _declarations.Count)
                {
                    yield return _declarations[i];
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

        #endregion
    }
}
