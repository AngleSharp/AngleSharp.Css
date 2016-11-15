namespace AngleSharp.Css.Dom
{
    using System;
    using System.Linq;

    public static class CssDocumentRuleExtensions
    {
        public static Boolean IsValid(this ICssDocumentRule rule, Url url)
        {
            return rule.Conditions.Any(m => m.Matches(url));
        }
    }
}
