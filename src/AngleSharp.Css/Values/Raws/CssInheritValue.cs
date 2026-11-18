#nullable disable
namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents the CSS inherit value.
    /// </summary>
    sealed class CssInheritValue : ICssSpecialValue, ICssComputedValue
    {
        #region ctor

        private CssInheritValue()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the only instance.
        /// </summary>
        public static readonly CssInheritValue Instance = new();

        /// <summary>
        /// Gets the referring value.
        /// </summary>
        public ICssValue Value => null;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => CssKeywords.Inherit;

        #endregion

        #region Methods

        Boolean ICssComputedValue.IsComputed => true;

        ICssValue ICssValue.Compute(ICssComputeContext context) => this;

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => Object.ReferenceEquals(this, other);

        #endregion
    }
}
