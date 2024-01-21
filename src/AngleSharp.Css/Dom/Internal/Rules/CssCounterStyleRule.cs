namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Represents the @counter-style rule.
    /// </summary>
    [DebuggerDisplay(null, Name = "CssCounterStyleRule ({StyleName})")]
    sealed class CssCounterStyleRule : CssDeclarationRule, ICssRule
    {
        #region ctor

        internal CssCounterStyleRule(ICssStyleSheet owner)
            : base(owner, CssRuleType.CounterStyle, RuleNames.CounterStyle, [])
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the counter style name argument.
        /// </summary>
        public String StyleName { get; set; }

        #endregion

        #region Methods

        protected override void ReplaceWith(ICssRule rule)
        {
        }

        #endregion
    }
}
