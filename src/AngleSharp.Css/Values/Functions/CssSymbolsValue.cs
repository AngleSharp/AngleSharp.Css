namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    /// <summary>
    /// Represents a CSS symbols function call.
    /// </summary>
    public sealed class CssSymbolsValue : ICssFunctionValue
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
        public ICssValue[] Arguments => _entries.Select(e => new Label(e) as ICssValue).ToArray();

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText =>Name.CssFunction(GetArgs());

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
