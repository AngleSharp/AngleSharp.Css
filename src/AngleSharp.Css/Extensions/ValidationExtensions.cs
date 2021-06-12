namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;
    using System.Linq;

    /// <summary>
    /// Extensions for validating CSS rules.
    /// </summary>
    public static class ValidationExtensions
    {
        /// <summary>
        /// Determines if the rule is valid for a given device.
        /// </summary>
        /// <param name="rule">The rule to extend.</param>
        /// <param name="device">The device to check for support.</param>
        /// <returns>True if support is given, otherwise false.</returns>
        public static Boolean IsValid(this ICssSupportsRule rule, IRenderDevice device) => rule.Condition.Check(device);

        /// <summary>
        /// Determines if the rule is valid for a given device.
        /// </summary>
        /// <param name="rule">The rule to extend.</param>
        /// <param name="device">The device to check for conformance.</param>
        /// <returns>True if support is given, otherwise false.</returns>
        public static Boolean IsValid(this ICssMediaRule rule, IRenderDevice device) => rule.Media.Validate(device);

        /// <summary>
        /// Determines if the rule is valid for a given URL.
        /// </summary>
        /// <param name="rule">The rule to extend.</param>
        /// <param name="url">The URL to check for conformance.</param>
        /// <returns>True if the URL is matching, otherwise false.</returns>
        public static Boolean IsValid(this ICssDocumentRule rule, Url url) => rule.Conditions.Any(m => m.Matches(url));
    }
}
