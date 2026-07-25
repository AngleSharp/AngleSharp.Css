namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents a @container CSS rule.
    /// </summary>
    [DomName("CSSContainerRule")]
    public interface ICssContainerRule : ICssConditionRule
    {
        /// <summary>
        /// Gets the optional container name.
        /// </summary>
        [DomName("containerName")]
        String ContainerName { get; }

        /// <summary>
        /// Gets the container query part.
        /// </summary>
        [DomName("containerQuery")]
        String ContainerQuery { get; }
    }
}
