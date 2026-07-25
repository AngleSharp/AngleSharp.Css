namespace AngleSharp.Css.Values
{
    using System;

    /// <summary>
    /// Defines a class of CSS values that have a metric.
    /// </summary>
    public interface ICssMetricValue : ICssPrimitiveValue
    {
        /// <summary>
        /// Gets the value of the metric.
        /// </summary>
        Double Value { get; }

        /// <summary>
        /// Gets the label of the unit.
        /// </summary>
        String UnitString { get; }
    }
}
