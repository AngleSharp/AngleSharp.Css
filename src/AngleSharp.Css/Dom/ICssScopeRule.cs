namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents a @scope CSS rule.
    /// </summary>
    [DomName("CSSScopeRule")]
    public interface ICssScopeRule : ICssGroupingRule
    {
        /// <summary>
        /// Gets or sets the scope prelude text.
        /// </summary>
        [DomName("scopeText")]
        String ScopeText { get; set; }
    }
}
