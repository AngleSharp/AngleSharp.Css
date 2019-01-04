namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a CSS var replacement.
    /// </summary>
    class VarReference : ICssValue, ICssFunctionValue
    {
        #region Fields

        private readonly String _variableName;
        private readonly String _defaultValue;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new variable reference.
        /// </summary>
        /// <param name="variableName">The name of the custom property.</param>
        /// <param name="defaultValue">The fallback value, if any.</param>
        public VarReference(String variableName, String defaultValue = null)
        {
            _variableName = variableName;
            _defaultValue = defaultValue;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name
        {
            get { return FunctionNames.Var; }
        }

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments
        {
            get
            {
                var list = new List<ICssValue>();
                list.Add(new Identifier(_variableName));

                if (_defaultValue != null)
                {
                    list.Add(new CssAnyValue(_defaultValue));
                }

                return list.ToArray();
            }
        }

        /// <summary>
        /// Gets the referenced variable name.
        /// </summary>
        public String VariableName
        {
            get { return _variableName; }
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
                args.Add(_variableName);

                if (!String.IsNullOrEmpty(_defaultValue))
                {
                    args.Add(_defaultValue);
                }

                return fn.CssFunction(String.Join(", ", args));
            }
        }

        #endregion
    }
}
