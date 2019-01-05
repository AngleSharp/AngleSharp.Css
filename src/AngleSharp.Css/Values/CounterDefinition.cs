namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a CSS counter.
    /// </summary>
    struct CounterDefinition : ICssValue
    {
        #region Fields

        private readonly String _identifier;
        private readonly String _listStyle;
        private readonly String _separator;

        #endregion

        #region ctor

        /// <summary>
        /// Specifies a counter value.
        /// </summary>
        /// <param name="identifier">The identifier of the counter.</param>
        /// <param name="listStyle">The used list style.</param>
        /// <param name="separator">The separator of the counter.</param>
        public CounterDefinition(String identifier, String listStyle, String separator)
        {
            _identifier = identifier;
            _listStyle = listStyle;
            _separator = separator;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get { return String.Concat(_identifier, " ", _listStyle, " ", _separator); }
        }

        /// <summary>
        /// Gets the identifier of the counter.
        /// </summary>
        public String CounterIdentifier
        {
            get { return _identifier; }
        }

        /// <summary>
        /// Gets the style of the counter.
        /// </summary>
        public String ListStyle
        {
            get { return _listStyle; }
        }

        /// <summary>
        /// Gets the defined separator of the counter.
        /// </summary>
        public String DefinedSeparator
        {
            get { return _separator; }
        }

        #endregion
    }
}
