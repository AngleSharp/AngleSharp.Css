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

        public String this[String propertyName]
        {
            get { return GetValue(propertyName); }
        }

        public Int32 Length
        {
            get { return _declarations.Count; }
        }

        #endregion

        #region Methods

        public String GetPropertyValue(String propertyName)
        {
            return GetValue(propertyName);
        }

        public String GetPropertyPriority(String propertyName)
        {
            return null;
        }

        public void SetProperty(String propertyName, String propertyValue, String priority = null)
        {
            SetValue(propertyName, propertyValue);
        }

        public String RemoveProperty(String propertyName)
        {
            for (var i = 0; i < _declarations.Count; i++)
            {
                var declaration = _declarations[i];

                if (declaration.Name.Is(propertyName))
                {
                    _declarations.RemoveAt(i);
                    return declaration.Value;
                }
            }

            return null;
        }

        public IEnumerator<ICssProperty> GetEnumerator()
        {
            return _declarations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var block = formatter.BlockDeclarations(_declarations);
            writer.Write(formatter.Rule(_name, null, block));
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
    }
}
