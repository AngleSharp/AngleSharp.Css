namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a CSS var replacement.
    /// </summary>
    public class VarReference : ICssValue
    {
        private readonly String _name;
        private readonly String _defaultValue;

        /// <summary>
        /// Creates a new variable reference.
        /// </summary>
        /// <param name="name">The name of the custom property.</param>
        /// <param name="defaultValue">The fallback value, if any.</param>
        public VarReference(String name, String defaultValue = null)
        {
            _name = name;
            _defaultValue = defaultValue;
        }

        /// <summary>
        /// Gets the referenced variable name.
        /// </summary>
        public String Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets the defined fallback value, if any.
        /// </summary>
        public String DefaultValue
        {
            get { return _defaultValue; }
        }

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var fn = FunctionNames.Var;
                var args = new List<String>();
                args.Add(_name);

                if (!String.IsNullOrEmpty(_defaultValue))
                {
                    args.Add(_defaultValue);
                }

                return fn.CssFunction(String.Join(", ", args));
            }
        }
    }
}
