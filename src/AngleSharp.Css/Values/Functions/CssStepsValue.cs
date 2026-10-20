#nullable disable
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
    public sealed class CssStepsValue : ICssTimingFunctionValue, IEquatable<CssStepsValue>
    {
        #region Fields

        private readonly ICssValue _intervals;
        private readonly Boolean _start;
        private readonly String _position;

        #endregion

        #region ctor

        /// <summary>
        /// The first parameter specifies the number of intervals in the function. 
        /// The second parameter specifies the point at which the change of values
        /// occur within the interval. 
        /// </summary>
        /// <param name="intervals">It must be a positive integer (greater than 0).</param>
        /// <param name="start">Optional: If not specified then the change occurs at the end.</param>
        public CssStepsValue(ICssValue intervals, Boolean start = false)
            : this(intervals, start ? CssKeywords.Start : null)
        {
        }

        /// <summary>
        /// Creates a new steps timing-function with the given step position keyword, e.g.,
        /// jump-none. A missing position is the same as end.
        /// </summary>
        internal CssStepsValue(ICssValue intervals, String position)
        {
            _intervals = intervals;
            _position = position;
            _start = position is not null && (position.Is(CssKeywords.Start) || position.Is(CssKeywords.JumpStart));
        }

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

                if (_position is not null && !_position.Is(CssKeywords.End) && !_position.Is(CssKeywords.JumpEnd))
                {
                    args.Add(new CssIdentifierValue(_position));
                }

                return args.ToArray();
            }
        }

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                if (!_intervals.Equals(CssIntegerValue.One) || (_position is not null && !_position.Is(CssKeywords.Start) && !_position.Is(CssKeywords.End)))
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
        public Boolean Equals(CssStepsValue other)
        {
            if (other is not null)
            {
                var comparer = EqualityComparer<ICssValue>.Default;
                return _start == other._start && comparer.Equals(_intervals, other._intervals) && NormalizedPosition.Is(other.NormalizedPosition);
            }

            return false;
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssStepsValue value && Equals(value);

        /// <summary>
        /// Gets the step position, where start and end are the same as jump-start and jump-end.
        /// </summary>
        private String NormalizedPosition => _start ? CssKeywords.Start :
            _position is null || _position.Is(CssKeywords.JumpEnd) ? CssKeywords.End : _position;

        ICssValue ICssValue.Compute(ICssComputeContext context) => this;

        #endregion
    }
}
