namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents a @color-profile CSS rule.
    /// </summary>
    [DomName("CSSColorProfileRule")]
    public interface ICssColorProfileRule : ICssRule, ICssProperties
    {
        /// <summary>
        /// Gets the profile name.
        /// </summary>
        [DomName("name")]
        String Name { get; }
    }
}
