namespace AngleSharp.Css.Dom
{
    using System;

    /// <summary>
    /// Represents a function of the @supports rule.
    /// </summary>
    public interface IConditionFunction : IStyleFormattable
    {
        /// <summary>
        /// Evaluates the condition and returns the result.
        /// </summary>
        /// <returns>True if the condition is supported, else false.</returns>
        Boolean Check(IRenderDevice device);
    }
}
