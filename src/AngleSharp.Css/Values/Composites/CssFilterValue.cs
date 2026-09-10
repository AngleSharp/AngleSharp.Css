#nullable disable
namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents a list of CSS filter functions.
    /// </summary>
    public sealed class CssFilterValue : ICssValue, IEquatable<CssFilterValue>
    {
        /// <summary>
        /// Creates a filter value from its functions.
        /// </summary>
        /// <param name="functions">The filter functions.</param>
        public CssFilterValue(ICssFilterFunctionValue[] functions)
        {
            Functions = functions ?? Array.Empty<ICssFilterFunctionValue>();
        }

        /// <summary>Gets the filter functions.</summary>
        public ICssFilterFunctionValue[] Functions { get; }

        /// <summary>Gets the serialized filter value.</summary>
        public String CssText => String.Join(" ", Functions.Select(function => function.CssText));

        /// <summary>Compares this filter value with another filter value.</summary>
        public Boolean Equals(CssFilterValue other) =>
            other is not null && CssText.Equals(other.CssText, StringComparison.OrdinalIgnoreCase);

        ICssValue ICssValue.Compute(ICssComputeContext context) => this;

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssFilterValue value && Equals(value);
    }
}
