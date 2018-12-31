namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;

    /// <summary>
    /// Represents an aggregator for CSS values (shorthand).
    /// </summary>
    public interface IValueAggregator
    {
        /// <summary>
        /// Distributes an associated value to multiple (longhand) values.
        /// </summary>
        /// <param name="value">The value to split.</param>
        /// <returns>The properties if the value can be split, otherwise null.</returns>
        ICssValue[] Split(ICssValue value);

        /// <summary>
        /// Collects several (longhand) values into a single value.
        /// </summary>
        /// <param name="values">The values to merge.</param>
        /// <returns>The values if a merge is possible, otherwise null.</returns>
        ICssValue Merge(ICssValue[] values);
    }
}
