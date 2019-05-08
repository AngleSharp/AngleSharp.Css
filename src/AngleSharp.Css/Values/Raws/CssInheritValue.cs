namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents the CSS inherit value.
    /// </summary>
    class CssInheritValue : ICssValue
    {
        private CssInheritValue()
        {
        }

        /// <summary>
        /// Gets the only instance.
        /// </summary>
        public static readonly CssInheritValue Instance = new CssInheritValue();

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => CssKeywords.Inherit;
    }
}
