namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Represents the @font-face rule.
    /// </summary>
    [DebuggerDisplay(null, Name = "CssFontFaceRule ({FontFamily})")]
    sealed class CssFontFaceRule : CssDeclarationRule, ICssFontFaceRule
    {
        #region Fields

        private static readonly HashSet<String> ContainedProperties = new HashSet<String>(StringComparer.OrdinalIgnoreCase)
        {
            PropertyNames.FontFamily,
            PropertyNames.Src,
            PropertyNames.FontStyle,
            PropertyNames.FontWeight,
            PropertyNames.FontStretch,
            PropertyNames.UnicodeRange,
            PropertyNames.FontVariant,
        };

        #endregion

        #region ctor

        internal CssFontFaceRule(ICssStyleSheet owner)
            : base(owner, CssRuleType.FontFace, RuleNames.FontFace, ContainedProperties)
        {
        }

        #endregion

        #region Properties

        internal String FontFamily => GetValue(PropertyNames.FontFamily);

        String ICssFontFaceRule.Family
        {
            get => GetValue(PropertyNames.FontFamily);
            set => SetValue(PropertyNames.FontFamily, value);
        }

        String ICssFontFaceRule.Source
        {
            get => GetValue(PropertyNames.Src);
            set => SetValue(PropertyNames.Src, value);
        }

        String ICssFontFaceRule.Style
        {
            get => GetValue(PropertyNames.FontStyle);
            set => SetValue(PropertyNames.FontStyle, value);
        }

        String ICssFontFaceRule.Weight
        {
            get => GetValue(PropertyNames.FontWeight);
            set => SetValue(PropertyNames.FontWeight, value);
        }

        String ICssFontFaceRule.Stretch
        {
            get => GetValue(PropertyNames.FontStretch);
            set => SetValue(PropertyNames.FontStretch, value);
        }

        String ICssFontFaceRule.Range
        {
            get => GetValue(PropertyNames.UnicodeRange);
            set => SetValue(PropertyNames.UnicodeRange, value);
        }

        String ICssFontFaceRule.Variant
        {
            get => GetValue(PropertyNames.FontVariant);
            set => SetValue(PropertyNames.FontVariant, value);
        }

        String ICssFontFaceRule.Features
        {
            get => String.Empty;
            set { }
        }

        #endregion

        #region Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = (ICssFontFaceRule)rule;
            SetValue(PropertyNames.FontFamily, newRule.Family);
            SetValue(PropertyNames.Src, newRule.Source);
            SetValue(PropertyNames.FontStyle, newRule.Style);
            SetValue(PropertyNames.FontWeight, newRule.Weight);
            SetValue(PropertyNames.FontStretch, newRule.Stretch);
            SetValue(PropertyNames.UnicodeRange, newRule.Range);
            SetValue(PropertyNames.FontVariant, newRule.Variant);
        }

        #endregion
    }
}
