namespace AngleSharp.Css.Aggregators
{
    using static ValueConverters;

    sealed class MarginAggregator : BoxBaseAggregator
    {
        public MarginAggregator()
            : base(PropertyNames.MarginTop, PropertyNames.MarginRight, PropertyNames.MarginBottom, PropertyNames.MarginLeft, MarginConverter, PropertyFlags.Unitless | PropertyFlags.Animatable)
        {
        }
    }
}