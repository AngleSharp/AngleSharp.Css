#nullable disable
namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;

    abstract class CssDescriptorRule : CssRule, ICssProperties
    {
        private readonly List<DescriptorProperty> _descriptors;
        private readonly String _ruleName;

        protected CssDescriptorRule(ICssStyleSheet owner, CssRuleType type, String ruleName)
            : base(owner, type)
        {
            _descriptors = new List<DescriptorProperty>();
            _ruleName = ruleName;
        }

        public String this[String propertyName] => GetPropertyValue(propertyName);

        public Int32 Length => _descriptors.Count;

        protected abstract String PreludeText { get; }

        public ICssProperty GetProperty(String propertyName)
        {
            for (var i = 0; i < _descriptors.Count; i++)
            {
                var descriptor = _descriptors[i];

                if (descriptor.Name.Is(propertyName))
                {
                    return descriptor;
                }
            }

            return null;
        }

        public String GetPropertyValue(String propertyName)
        {
            var property = GetProperty(propertyName);
            return property?.Value ?? String.Empty;
        }

        public String GetPropertyPriority(String propertyName)
        {
            var property = GetProperty(propertyName);
            return property?.IsImportant == true ? CssKeywords.Important : null;
        }

        public void SetProperty(String propertyName, String propertyValue, String priority = null)
        {
            if (String.IsNullOrEmpty(propertyName))
            {
                return;
            }

            var property = (DescriptorProperty)GetProperty(propertyName);

            if (String.IsNullOrEmpty(propertyValue))
            {
                if (property is not null)
                {
                    _descriptors.Remove(property);
                }

                return;
            }

            if (property is null)
            {
                property = new DescriptorProperty(propertyName);
                _descriptors.Add(property);
            }

            property.Value = propertyValue;
            property.IsImportant = priority.Is(CssKeywords.Important);
        }

        public String RemoveProperty(String propertyName)
        {
            for (var i = 0; i < _descriptors.Count; i++)
            {
                var descriptor = _descriptors[i];

                if (descriptor.Name.Is(propertyName))
                {
                    _descriptors.RemoveAt(i);
                    return descriptor.Value;
                }
            }

            return null;
        }

        public IEnumerator<ICssProperty> GetEnumerator()
        {
            foreach (var descriptor in _descriptors)
            {
                yield return descriptor;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var rules = formatter.BlockDeclarations(_descriptors);
            writer.Write(formatter.Rule(_ruleName, PreludeText, rules));
        }

        protected void ReplaceWith(ICssProperties properties)
        {
            _descriptors.Clear();

            foreach (var property in properties)
            {
                SetProperty(property.Name, property.Value, property.IsImportant ? CssKeywords.Important : null);
            }
        }

        private sealed class DescriptorProperty : ICssProperty
        {
            private readonly String _name;

            public DescriptorProperty(String name)
            {
                _name = name;
            }

            public String Name => _name;

            public ICssValue RawValue => null;

            public String Value { get; set; } = String.Empty;

            public Boolean IsImportant { get; set; }

            public Boolean IsInherited => false;

            public Boolean IsInitial => false;

            public Boolean IsAnimatable => false;

            public Boolean CanBeInherited => false;

            public Boolean IsShorthand => false;

            public ICssProperty Compute(ICssComputeContext context) => this;

            public void ToCss(TextWriter writer, IStyleFormatter formatter)
            {
                writer.Write(formatter.Declaration(Name, Value, IsImportant));
            }
        }
    }
}
