namespace AngleSharp.Css.Aggregators
{
    using AngleSharp.Css.Declarations;
    using static ValueConverters;

    sealed class PaddingAggregator : BoxBaseAggregator
    {
        public PaddingAggregator()
            : base(PaddingTopDeclaration.Name, PaddingRightDeclaration.Name, PaddingBottomDeclaration.Name, PaddingLeftDeclaration.Name, PaddingConverter, PropertyFlags.Unitless | PropertyFlags.Animatable)
        {
        }
    }
}