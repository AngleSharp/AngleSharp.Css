namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a CSS symbols function call.
    /// </summary>
    public sealed class CssSymbolsValue : ICssFunctionValue, IEquatable<CssSymbolsValue>
    {
        #region Fields

        private readonly ICssValue _type;
        private readonly List<String> _entries;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new sybols function call.
        /// </summary>
        /// <param name="type">The used type.</param>
        /// <param name="entries">The contained entries.</param>
        public CssSymbolsValue(ICssValue type, List<String> entries)
        {
            _type = type;
            _entries = entries;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the used type, if any.
        /// </summary>
        public ICssValue Type => _type;

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name => FunctionNames.Symbols;

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ICssValue[] Arguments => _entries.Select(e => new CssStringValue(e) as ICssValue).ToArray();

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText =>Name.CssFunction(GetArgs());

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssSymbolsValue other)
        {
            var l = _entries.Count;

            if (_type.Equals(other._type) && l == other._entries.Count)
            {
                for (var i = 0; i < l; i++)
                {
                    var a = _entries[i];
                    var b = other._entries[i];

                    if (!a.Equals(b))
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssSymbolsValue value && Equals(value);

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            if (_type == null)
            {
                var type = new CssConstantValue<SymbolsType>(CssKeywords.Cyclic, SymbolsType.Cyclic);
                return new CssSymbolsValue(type, _entries);
            }

            return this;
        }

        #endregion

        #region Helpers

        private String GetArgs()
        {
            var arg = String.Join(" ", _entries.Select(m => m.CssString()));
            return _type is not null ? $"{_type.CssText} {arg}" : arg;
        }

        #endregion
    }
}
