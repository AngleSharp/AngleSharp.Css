#nullable enable
namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents a relative font weight (bolder or lighter), which computes to
    /// a number resolved against the parent's computed weight.
    /// See https://www.w3.org/TR/css-fonts-4/#relative-weights.
    /// </summary>
    sealed class CssRelativeFontWeightValue : ICssPrimitiveValue, IEquatable<CssRelativeFontWeightValue>
    {
        #region Fields

        private readonly CssConstantValue<FontWeight> _keyword;

        #endregion

        #region ctor

        public CssRelativeFontWeightValue(CssConstantValue<FontWeight> keyword)
        {
            _keyword = keyword;
        }

        #endregion

        #region Properties

        public FontWeight Value => _keyword.Value;

        public String CssText => _keyword.CssText;

        #endregion

        #region Methods

        public Boolean Equals(CssRelativeFontWeightValue? other) => other is not null && _keyword.Equals(other._keyword);

        Boolean IEquatable<ICssValue>.Equals(ICssValue? other) => other is CssRelativeFontWeightValue value && Equals(value);

        public override Boolean Equals(Object? obj) => obj is CssRelativeFontWeightValue value && Equals(value);

        public override Int32 GetHashCode() => _keyword.GetHashCode();

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var parent = (context as IInheritingComputeContext)?.InheritedValue(PropertyNames.FontWeight);
            var weight = GetWeight(parent);
            var result = Value == FontWeight.Bolder ? Bolder(weight) : Lighter(weight);
            return new CssIntegerValue(result);
        }

        private static Int32 Bolder(Int32 weight) => weight switch
        {
            < 350 => 400,
            < 550 => 700,
            < 900 => 900,
            _ => weight,
        };

        private static Int32 Lighter(Int32 weight) => weight switch
        {
            < 100 => weight,
            < 550 => 100,
            < 750 => 400,
            _ => 700,
        };

        private static Int32 GetWeight(ICssValue? value) => value switch
        {
            CssIntegerValue integer => integer.IntValue,
            CssConstantValue<FontWeight> constant => constant.Value == FontWeight.Bold ? 700 : 400,
            ICssMultipleValue multiple when multiple.Count > 0 => GetWeight(multiple.First()),
            _ => 400,
        };

        #endregion
    }
}
