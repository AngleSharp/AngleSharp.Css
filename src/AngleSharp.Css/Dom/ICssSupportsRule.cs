namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;

    /// <summary>
    /// Represents a ‘@supports’ CSS rule.
    /// </summary>
    [DomName("CSSSupportsRule")]
    public interface ICssSupportsRule : ICssConditionRule
    {
        /// <summary>
        /// Gets the condition of the supports rule.
        /// </summary>
        IConditionFunction Condition { get; }
    }
}
