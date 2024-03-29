namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a CSS var replacement.
    /// </summary>
    public sealed class CssVarValue : ICssFunctionValue, IEquatable<CssVarValue>
    {
        #region Fields

        private readonly String _variableName;
        private readonly ICssValue _defaultValue;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new variable reference.
        /// </summary>
        /// <param name="variableName">The name of the custom property.</param>
        /// <param name="defaultValue">The fallback value, if any.</param>
        public CssVarValue(String variableName, ICssValue defaultValue = null)
        {
            _variableName = variableName;
            _defaultValue = defaultValue;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name => FunctionNames.Var;

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments
        {
            get
            {
                var list = new List<ICssValue>
                {
                    new CssIdentifierValue(_variableName),
                };

                if (_defaultValue != null)
                {
                    list.Add(_defaultValue);
                }

                return list.ToArray();
            }
        }

        /// <summary>
        /// Gets the referenced variable name.
        /// </summary>
        public String VariableName => _variableName;

        /// <summary>
        /// Gets the defined fallback value, if any.
        /// </summary>
        public ICssValue DefaultValue => _defaultValue;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var fn = FunctionNames.Var;
                var args = new List<String>
                {
                    _variableName,
                };

                if (_defaultValue is not null)
                {
                    args.Add(_defaultValue.CssText);
                }

                return fn.CssFunction(String.Join(", ", args));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssVarValue other)
        {
            if (other is not null)
            {
                var comparer = EqualityComparer<ICssValue>.Default;
                return comparer.Equals(_defaultValue, other._defaultValue) && _variableName == other._variableName;
            }

            return false;
        }

        /// <summary>
        /// Resolves the value of the referenced variable. Returns null
        /// if the reference is invalid or cannot be resolved.
        /// </summary>
        /// <param name="context">The context to use for resolving the variable.</param>
        /// <returns>The resolved value or null.</returns>
        public ICssValue Compute(ICssComputeContext context)
        {
            var value = context.Resolve(_variableName)?.Compute(context);

            if (value is not null)
            {
                return value;
            }

            return _defaultValue?.Compute(context);
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssVarValue value && Equals(value);

        #endregion
    }
}
