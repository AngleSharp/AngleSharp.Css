namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Represents the @font-feature-values rule.
    /// </summary>
    [DebuggerDisplay(null, Name = "CssFontFeatureValuesRule ({FamilyName})")]
    sealed class CssFontFeatureValuesRule : CssDeclarationRule, ICssRule
    {
        #region ctor

        internal CssFontFeatureValuesRule(ICssStyleSheet owner)
            : base(owner, CssRuleType.FontFeatureValues, RuleNames.FontFeatureValues, [])
        {
            // character-variant, styleset, stylistic, ornaments, annotation, swash
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the family name argument.
        /// </summary>
        public String FamilyName { get; set; }

        #endregion

        #region Methods

        protected override void ReplaceWith(ICssRule rule)
        {
        }

        #endregion
    }
}
