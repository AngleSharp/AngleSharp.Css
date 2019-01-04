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
    class StepsTimingFunction : ITimingFunction
    {
        #region Fields

        private readonly Int32 _intervals;
        private readonly Boolean _start;

        #endregion

        #region ctor

        /// <summary>
        /// The first parameter specifies the number of intervals in the function. 
        /// The second parameter specifies the point at which the change of values
        /// occur within the interval. 
        /// </summary>
        /// <param name="intervals">It must be a positive integer (greater than 0).</param>
        /// <param name="start">Optional: If not specified then the change occurs at the end.</param>
        public StepsTimingFunction(Int32 intervals, Boolean start = false)
        {
            _intervals = Math.Max(1, intervals);
            _start = start;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name
        {
            get { return FunctionNames.Steps; }
        }

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments
        {
            get
            {
                var args = new List<ICssValue>();
                args.Add(new Length(_intervals, Length.Unit.None));

                if (_start)
                {
                    args.Add(new Identifier(CssKeywords.Start));
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
                if (_intervals != 1)
                {
                    var fn = FunctionNames.Steps;
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
        public Int32 Intervals
        {
            get { return _intervals; }
        }

        /// <summary>
        /// Gets if the steps should occur in the beginning.
        /// </summary>
        public Boolean IsStart
        {
            get { return _start; }
        }

        #endregion
    }
}
