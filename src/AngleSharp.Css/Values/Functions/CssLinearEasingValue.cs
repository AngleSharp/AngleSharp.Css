#nullable disable
namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents the linear() easing function, i.e., a piecewise linear timing function.
    /// https://drafts.csswg.org/css-easing-2/#the-linear-easing-function
    /// </summary>
    sealed class CssLinearEasingValue : ICssTimingFunctionValue, IEquatable<CssLinearEasingValue>
    {
        #region Fields

        private readonly CssTupleValue[] _stops;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new linear easing function.
        /// </summary>
        /// <param name="stops">The stops, each an output number with up to two input percentages.</param>
        public CssLinearEasingValue(CssTupleValue[] stops)
        {
            _stops = stops;
        }

        #endregion

        #region Properties

        public String Name => CssKeywords.Linear;

        public ICssValue[] Arguments => _stops;

        public String CssText => Name.CssFunction(Arguments.Join(", "));

        #endregion

        #region Methods

        public Boolean Equals(CssLinearEasingValue other) =>
            other is not null && _stops.Length == other._stops.Length && _stops.Select((m, i) => m.CssText.Is(other._stops[i].CssText)).All(m => m);

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssLinearEasingValue value && Equals(value);

        ICssValue ICssValue.Compute(ICssComputeContext context) => this;

        #endregion
    }
}
