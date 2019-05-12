namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;

    /// <summary>
    /// Represents a special CSS value.
    /// </summary>
    public interface ICssSpecialValue : ICssValue
    {
        /// <summary>
        /// Gets the underlying CSS value the special one
        /// is referring to (e.g., the initial value).
        /// </summary>
        ICssValue Value { get; }
    }
}
