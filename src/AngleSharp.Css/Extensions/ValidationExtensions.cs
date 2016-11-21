namespace AngleSharp.Css.Extensions
{
    using AngleSharp.Css.Dom;
    using System;

    public static class ValidationExtensions
    {
        public static Boolean IsValid(this ICssSupportsRule rule, IRenderDevice device)
        {
            return rule.Condition.Check(device);
        }

        public static Boolean IsValid(this ICssMediaRule rule, IRenderDevice device)
        {
            return rule.Media.Validate(device);
        }
    }
}
