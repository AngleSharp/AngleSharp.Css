namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS repeat function call.
    /// </summary>
    class Repeat : ICssFunctionValue
    {
        #region Fields

        private readonly ICssValue _count;
        private readonly ICssValue _value;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new repeat function call.
        /// </summary>
        /// <param name="count">The count value.</param>
        /// <param name="value">The used value.</param>
        public Repeat(ICssValue count, ICssValue value)
        {
            _count = count;
            _value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the used count.
        /// </summary>
        public ICssValue Count => _count;

        /// <summary>
        /// Gets the used value.
        /// </summary>
        public ICssValue Value => _value;

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name => FunctionNames.Repeat;

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments => new[] { _count, _value };

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => Name.CssFunction(Arguments.Join(", "));

        #endregion
    }
}
