namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Extensions;
    using AngleSharp.Dom;
    using System;
    using System.IO;

    /// <summary>
    /// Represents an @namespace rule.
    /// </summary>
    sealed class CssNamespaceRule : CssRule, ICssNamespaceRule
    {
        #region Fields

        private String _namespaceUri;
        private String _prefix;

        #endregion

        #region ctor

        internal CssNamespaceRule(ICssStyleSheet owner)
            : base(owner, CssRuleType.Namespace)
        {
            _namespaceUri = String.Empty;
            _prefix = String.Empty;
        }

        #endregion

        #region Properties

        public String NamespaceUri
        {
            get { return _namespaceUri; }
            set { CheckValidity(); _namespaceUri = value ?? String.Empty; }
        }

        public String Prefix
        {
            get { return _prefix; }
            set { CheckValidity(); _prefix = value ?? String.Empty; }
        }

        #endregion

        #region Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = (ICssNamespaceRule)rule;
            _namespaceUri = newRule.NamespaceUri;
            _prefix = newRule.Prefix;
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var space = String.IsNullOrEmpty(_prefix) ? String.Empty : " ";
            var value = String.Concat(_prefix, space, _namespaceUri.CssUrl());
            writer.Write(formatter.Rule(RuleNames.Namespace, value));
        }

        #endregion

        #region Helpers

        private static Boolean IsNotSupported(CssRuleType type)
        {
            return type != CssRuleType.Charset && type != CssRuleType.Import && type != CssRuleType.Namespace;
        }

        private void CheckValidity()
        {
            var parent = Owner;
            var list = parent != null ? parent.Rules : null;

            if (list != null)
            {
                foreach (var entry in list)
                {
                    if (IsNotSupported(entry.Type))
                        throw new DomException(DomError.InvalidState);
                }
            }
        }

        #endregion
    }
}
