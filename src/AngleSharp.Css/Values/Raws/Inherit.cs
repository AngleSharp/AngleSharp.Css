namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents the CSS inherit value.
    /// </summary>
    class Inherit : ICssValue
    {
        private Inherit()
        {
        }

        /// <summary>
        /// Gets the only instance.
        /// </summary>
        public static readonly Inherit Instance = new Inherit();

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => CssKeywords.Inherit;
    }
}
