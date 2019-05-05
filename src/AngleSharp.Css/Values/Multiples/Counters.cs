namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents the CSS counters definition.
    /// </summary>
    class Counters : ICssMultipleValue
    {
        private readonly ICssValue[] _counters;

        /// <summary>
        /// Creates an empty CSS counters definition.
        /// </summary>
        public Counters()
        {
            _counters = null;
        }

        /// <summary>
        /// Creates a CSS counters definition.
        /// </summary>
        /// <param name="counters">The counters to contain.</param>
        public Counters(ICssValue[] counters)
        {
            _counters = counters;
        }

        /// <summary>
        /// Gets the values of the contained counters.
        /// </summary>
        public ICssValue[] Values => _counters;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => _counters != null ? _counters.Join(" ") : CssKeywords.None;
    }
}
