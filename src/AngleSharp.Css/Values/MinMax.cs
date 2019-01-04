namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS minmax function call.
    /// </summary>
    class Minmax : ICssFunctionValue
    {
        #region Fields

        private readonly ICssValue _min;
        private readonly ICssValue _max;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new minmax function call.
        /// </summary>
        /// <param name="min">The used lower bound.</param>
        /// <param name="max">The used upper bound.</param>
        public Minmax(ICssValue min, ICssValue max)
        {
            _min = min;
            _max = max;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the upper limit.
        /// </summary>
        public ICssValue Maximum => _max;

        /// <summary>
        /// Gets the lower limit.
        /// </summary>
        public ICssValue Minimum => _min;

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name => FunctionNames.Minmax;

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments => new[] { _min, _max };

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => Name.CssFunction(Arguments.Join(", "));

        #endregion
    }
}
