#nullable disable
namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Represents a CSS @property rule.
    /// https://www.w3.org/TR/css-properties-values-api-1/#at-property-rule
    /// </summary>
    [DebuggerDisplay(null, Name = "CssPropertyRule ({Name})")]
    sealed class CssPropertyRule : CssDescriptorRule, ICssPropertyRule
    {
        internal const String SyntaxDescriptor = "syntax";
        internal const String InheritsDescriptor = "inherits";
        internal const String InitialValueDescriptor = "initial-value";

        internal CssPropertyRule(ICssStyleSheet owner)
            : base(owner, CssRuleType.Property, RuleNames.Property)
        {
        }

        private String _name;

        public String Name
        {
            get => _name;
            set
            {
                _name = value;
                CssPropertyRegistryCache.Invalidate();
            }
        }

        public String Syntax => ParseSyntaxString(GetPropertyValue(SyntaxDescriptor)) ?? String.Empty;

        public Boolean Inherits => GetPropertyValue(InheritsDescriptor).Isi(CssKeywords.True);

        public String InitialValue => GetProperty(InitialValueDescriptor)?.Value;

        public Boolean IsValid => CreateRegistration() is not null;

        protected override String PreludeText => Name;

        /// <summary>
        /// Creates the registration of the rule, if the rule is valid: it needs a valid syntax
        /// and inherits, and an initial value that matches the syntax and is computationally
        /// independent, unless the syntax is universal.
        /// </summary>
        internal CssPropertyRegistration CreateRegistration()
        {
            var syntaxText = ParseSyntaxString(GetPropertyValue(SyntaxDescriptor));
            var syntax = syntaxText is not null ? CssPropertySyntax.Parse(syntaxText) : null;

            if (!CssPropertyRegistration.IsCustomPropertyName(Name) || syntax is null || GetProperty(InheritsDescriptor) is null)
            {
                return null;
            }

            return CssPropertyRegistration.Create(Name, syntax, Inherits, GetProperty(InitialValueDescriptor)?.Value, CssPropertyRegistrationOrigin.Rule);
        }

        /// <summary>
        /// Drops invalid descriptors (CSS Properties and Values API 3): syntax has to be a
        /// valid syntax string, inherits either true or false, and unknown descriptors are ignored.
        /// </summary>
        protected override Boolean IsValidDescriptor(String name, String value)
        {
            if (name.Isi(SyntaxDescriptor))
            {
                var text = ParseSyntaxString(value);
                return text is not null && CssPropertySyntax.Parse(text) is not null;
            }
            else if (name.Isi(InheritsDescriptor))
            {
                var keyword = value.Trim();
                return keyword.Isi(CssKeywords.True) || keyword.Isi(CssKeywords.False);
            }

            return name.Isi(InitialValueDescriptor);
        }

        // the registrations of documents may change
        protected override void OnDescriptorsChanged() => CssPropertyRegistryCache.Invalidate();

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = (ICssPropertyRule)rule;
            Name = newRule.Name;
            ReplaceWith((ICssProperties)newRule);
        }

        // the syntax descriptor is a <string>, e.g., "<length>"
        private static String ParseSyntaxString(String value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return null;
            }

            var source = new StringSource(value);
            source.SkipSpacesAndComments();
            var text = source.ParseString();
            source.SkipSpacesAndComments();
            return source.IsDone ? text : null;
        }
    }
}
