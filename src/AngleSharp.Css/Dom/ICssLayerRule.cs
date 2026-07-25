namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents a @layer CSS rule.
    /// </summary>
    [DomName("CSSLayerRule")]
    public interface ICssLayerRule : ICssGroupingRule
    {
        /// <summary>
        /// Gets the optional layer name or list of layer names.
        /// </summary>
        [DomName("name")]
        String Name { get; }

        /// <summary>
        /// Gets if the rule is a statement at-rule.
        /// </summary>
        [DomName("statement")]
        Boolean IsStatement { get; }
    }
}
