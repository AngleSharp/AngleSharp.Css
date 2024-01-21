namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a steps timing-function object.
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/timing-function
    /// </summary>
    /// <remarks>
    /// The first parameter specifies the number of intervals in the function. 
    /// The second parameter specifies the point at which the change of values
    /// occur within the interval. 
    /// </remarks>
    /// <param name="intervals">It must be a positive integer (greater than 0).</param>
    /// <param name="start">Optional: If not specified then the change occurs at the end.</param>
    sealed class CssStepsValue(ICssValue intervals, Boolean start = false) : ICssTimingFunctionValue, IEquatable<CssStepsValue>
    {
        #region Fields

        private readonly ICssValue _intervals = intervals;
        private readonly Boolean _start = start;

        #endregion
        
        #region ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name => FunctionNames.Steps;

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments
        {
            get
            {
                var args = new List<ICssValue>
                {
                    _intervals
                };

                if (_start)
                {
                    args.Add(new CssIdentifierValue(CssKeywords.Start));
                }

                return [.. args];
            }
        }

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                if (!_intervals.Equals(CssIntegerValue.One))
                {
                    return Name.CssFunction(Arguments.Join(", "));
                }
                else if (_start)
                {
                    return CssKeywords.StepStart;
                }
                else
                {
                    return CssKeywords.StepEnd;
                }
            }
        }

        /// <summary>
        /// Gets the numbers of intervals.
        /// </summary>
        public ICssValue Intervals => _intervals;

        /// <summary>
        /// Gets if the steps should occur in the beginning.
        /// </summary>
        public Boolean IsStart => _start;

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssStepsValue? other) => other is not null && _start == other._start && _intervals.Equals(other._intervals);

        Boolean IEquatable<ICssValue>.Equals(ICssValue? other) => other is CssStepsValue value && Equals(value);

        ICssValue ICssValue.Compute(ICssComputeContext context) => this;

        #endregion
    }
}
