#nullable disable
namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    sealed class CssNoneValue : ICssImageValue, ICssComputedValue
    {
        #region Properties

        public String Name => CssKeywords.None;

        public ICssValue[] Arguments => Array.Empty<ICssValue>();

        public String CssText => CssKeywords.None;

        #endregion

        #region Methods

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => Object.ReferenceEquals(this, other);

        Boolean ICssComputedValue.IsComputed => true;

        ICssValue ICssValue.Compute(ICssComputeContext context) => this;

        #endregion
    }
}
