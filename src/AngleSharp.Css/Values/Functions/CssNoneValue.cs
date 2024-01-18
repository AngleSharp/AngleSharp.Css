namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    sealed class CssNoneValue : ICssImageValue
    {
        #region Properties

        public String Name => CssKeywords.None;

        public ICssValue[] Arguments => Array.Empty<ICssValue>();

        public String CssText => CssKeywords.None;

        #endregion

        #region Methods

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            return this;
        }

        #endregion
    }
}
