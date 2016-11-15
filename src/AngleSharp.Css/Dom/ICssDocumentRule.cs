namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;

    /// <summary>
    /// Represents a document CSS rule.
    /// </summary>
    [DomName("CSSDocumentRule")]
    public interface ICssDocumentRule : ICssConditionRule
    {
        /// <summary>
        /// Gets the functions to evaluate as conditions.
        /// </summary>
        IDocumentFunctions Conditions { get; }
    }
}
