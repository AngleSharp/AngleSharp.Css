namespace AngleSharp.Css.Dom
{
    /// <summary>
    /// Represents the abstract base class for CSS media and CSS supports rules.
    /// </summary>
    abstract class CssConditionRule : CssGroupingRule
    {
        internal CssConditionRule (ICssStyleSheet owner, CssRuleType type)
            : base(owner, type)
	    { 
        }
    }
}
