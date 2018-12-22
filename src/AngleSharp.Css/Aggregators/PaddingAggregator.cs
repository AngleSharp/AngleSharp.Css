namespace AngleSharp.Css.Aggregators
{
    using static ValueConverters;

    sealed class PaddingAggregator : BoxBaseAggregator
    {
        public PaddingAggregator()
            : base(PropertyNames.PaddingTop, PropertyNames.PaddingRight, PropertyNames.PaddingBottom, PropertyNames.PaddingLeft, PaddingConverter, PropertyFlags.Unitless | PropertyFlags.Animatable)
        {
        }
    }
}