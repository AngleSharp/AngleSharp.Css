namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS calculated value.
    /// </summary>
    public sealed class Calc : ICssRawValue
    {
        private readonly ICssValue _argument;

        /// <summary>
        /// Creates a new calc function.
        /// </summary>
        /// <param name="argument">The argument to use.</param>
        public Calc(ICssValue argument)
        {
            _argument = argument;
        }

        /// <summary>
        /// Gets the argument of the calc function.
        /// </summary>
        public ICssValue Argument
        {
            get { return _argument; }
        }

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var argument = _argument.CssText;
                return FunctionNames.Calc.CssFunction(argument);
            }
        }
    }
}
