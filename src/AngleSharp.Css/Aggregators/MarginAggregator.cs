namespace AngleSharp.Css.Aggregators
{
    using AngleSharp.Css.Declarations;
    using static ValueConverters;

    sealed class MarginAggregator : BoxBaseAggregator
    {
        public MarginAggregator()
            : base(MarginTopDeclaration.Name, MarginRightDeclaration.Name, MarginBottomDeclaration.Name, MarginLeftDeclaration.Name, MarginConverter, PropertyFlags.Unitless | PropertyFlags.Animatable)
        {
        }
    }
}