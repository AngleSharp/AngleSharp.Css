namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents the CSS inherit value.
    /// </summary>
    sealed class CssInheritValue : ICssSpecialValue
    {
        private CssInheritValue()
        {
        }

        /// <summary>
        /// Gets the only instance.
        /// </summary>
        public static readonly CssInheritValue Instance = new CssInheritValue();

        /// <summary>
        /// Gets the referring value.
        /// </summary>
        public ICssValue Value => null;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => CssKeywords.Inherit;
    }
}
