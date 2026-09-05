namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    // Unlike a missing declaration, guaranteed-invalid is inherited as-is and
    // cannot be repaired by resolving its original references on a descendant.
    sealed class CssInvalidValue : ICssValue
    {
        public static readonly CssInvalidValue Instance = new();

        private CssInvalidValue()
        {
        }

        public String CssText => String.Empty;

        public ICssValue Compute(ICssComputeContext context) => this;

        public Boolean Equals(ICssValue? other) => other is CssInvalidValue;
    }
}
