namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Represents the base class for all style-rule similar rules.
    /// </summary>
    abstract class CssDeclarationRule : CssRule, ICssProperties
    {
        #region Fields

        private readonly List<ICssProperty> _declarations;
        private readonly String _name;

        #endregion

        #region ctor

        internal CssDeclarationRule(ICssStyleSheet owner, CssRuleType type, String name)
            : base(owner, type)
        {
            _declarations = new List<ICssProperty>();
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

        public void SetProperty(ICssProperty property)
        {
            for (var i = 0; i < _declarations.Count; i++)
            {
                if (_declarations[i].Name.Is(property.Name))
                {
                    _declarations[i] = property;
                    return;
                }
            }
            
            _declarations.Add(property);
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

        public ICssProperty CreateProperty(String name)
        {
            return CreateNewProperty(name);
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
            var rules = new FormatTransporter(_declarations);
            var content = formatter.Style(_name, rules);
            writer.Write(content);
        }

        #endregion

        #region Helpers

        private struct FormatTransporter : IStyleFormattable
        {
            private readonly IEnumerable<ICssProperty> _properties;

            public FormatTransporter(IEnumerable<ICssProperty> properties)
            {
                _properties = properties;
            }

            public void ToCss(TextWriter writer, IStyleFormatter formatter)
            {
                var properties = _properties.Select(m => m.ToCss(formatter));
                var content = formatter.Declarations(properties);
                writer.Write(content);
            }
        }

        protected abstract ICssProperty CreateNewProperty(String name);

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
