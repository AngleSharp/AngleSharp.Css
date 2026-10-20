#nullable disable
namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents a comparison function, i.e., min(), max(), or clamp().
    /// https://drafts.csswg.org/css-values-4/#comp-func
    /// </summary>
    sealed class CssMathValue : ICssFunctionValue
    {
        #region Fields

        private readonly String _name;
        private readonly ICssValue[] _arguments;

        #endregion

        #region ctor

        public CssMathValue(String name, ICssValue[] arguments)
        {
            _name = name;
            _arguments = arguments;
        }

        #endregion

        #region Properties

        public String Name => _name;

        public ICssValue[] Arguments => _arguments;

        public String CssText => _name.CssFunction(_arguments.Join(", "));

        #endregion

        #region Methods

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var arguments = _arguments.Select(m => m.Compute(context)).ToArray();

            if (arguments.All(m => m is ICssMetricValue) && arguments.Length > 0)
            {
                var metrics = arguments.Cast<ICssMetricValue>().ToArray();
                var unit = metrics[0].UnitString;

                if (metrics.All(m => m.UnitString == unit))
                {
                    if (_name.Is(FunctionNames.Min))
                    {
                        return metrics[0].WithValue(metrics.Min(m => m.Value));
                    }
                    else if (_name.Is(FunctionNames.Max))
                    {
                        return metrics[0].WithValue(metrics.Max(m => m.Value));
                    }
                    else if (_name.Is(FunctionNames.Clamp) && metrics.Length == 3)
                    {
                        // clamp(MIN, VAL, MAX) is max(MIN, min(VAL, MAX))
                        return metrics[0].WithValue(Math.Max(metrics[0].Value, Math.Min(metrics[1].Value, metrics[2].Value)));
                    }
                }
            }

            // The arguments cannot be compared yet (e.g., a percentage of an unknown box).
            return arguments.Any(m => m is null) ? null : new CssMathValue(_name, arguments);
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => Object.ReferenceEquals(this, other);

        #endregion
    }
}
