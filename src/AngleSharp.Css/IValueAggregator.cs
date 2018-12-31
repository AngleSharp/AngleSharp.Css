namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using System.Collections.Generic;

    /// <summary>
    /// Represents an aggregator for CSS values (shorthand).
    /// </summary>
    public interface IValueAggregator
    {
        /// <summary>
        /// Distributes an associated value to multiple (longhand) values.
        /// </summary>
        /// <param name="value">The value to distribute.</param>
        /// <returns>The properties if the value can be distributed, otherwise null.</returns>
        ICssValue[] Distribute(ICssValue value);

        /// <summary>
        /// Collects several (longhand) values into a single value.
        /// </summary>
        /// <param name="values">The values to collect.</param>
        /// <returns>The values if a collection is possible, otherwise null.</returns>
        ICssValue Collect(ICssValue[] values);
    }
}
