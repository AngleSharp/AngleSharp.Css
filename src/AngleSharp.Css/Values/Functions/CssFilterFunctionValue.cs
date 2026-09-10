#nullable disable
namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a CSS filter function.
    /// </summary>
    public sealed class CssFilterFunctionValue : ICssFilterFunctionValue, IEquatable<CssFilterFunctionValue>
    {
        private readonly String _name;
        private readonly ICssValue[] _arguments;
        private readonly String _cssText;

        /// <summary>
        /// Creates a filter function value.
        /// </summary>
        /// <param name="name">The function name.</param>
        /// <param name="arguments">The function arguments.</param>
        /// <param name="cssText">The serialized function.</param>
        public CssFilterFunctionValue(String name, ICssValue[] arguments, String cssText)
        {
            _name = name;
            _arguments = arguments ?? Array.Empty<ICssValue>();
            _cssText = cssText;
        }

        /// <summary>Gets the function name.</summary>
        public String Name => _name;

        /// <summary>Gets the function arguments.</summary>
        public ICssValue[] Arguments => _arguments;

        /// <summary>Gets the serialized function.</summary>
        public String CssText => _cssText;

        /// <summary>Compares this function with another function.</summary>
        public Boolean Equals(CssFilterFunctionValue other) =>
            other is not null && String.Equals(_cssText, other._cssText, StringComparison.OrdinalIgnoreCase);

        ICssValue ICssValue.Compute(ICssComputeContext context) => this;

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssFilterFunctionValue value && Equals(value);
    }
}
