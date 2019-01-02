namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS value using a function call.
    /// </summary>
    public class FunctionValue : ICssFunctionValue
    {
        private readonly String _name;
        private readonly ICssValue[] _arguments;

        /// <summary>
        /// Creates a new CSS function call value.
        /// </summary>
        /// <param name="name">The name of the called function.</param>
        /// <param name="arguments">The arguments to use.</param>
        public FunctionValue(String name, ICssValue[] arguments)
        {
            _name = name;
            _arguments = arguments;
        }

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets the arguments used for calling the function.
        /// </summary>
        public ICssValue[] Arguments
        {
            get { return _arguments; }
        }

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var argument = _arguments.Join(", ");
                return _name.CssFunction(argument);
            }
        }
    }
}
